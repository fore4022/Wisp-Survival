using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
/// <summary>
/// <para>
/// 모든 몬스터에 대한 기본 구현
/// </para>
/// 모든 몬스터는 ObjectPool을 통해서 생성되는 과정에서 일부 초기화 작업을 진행
/// 생성 이후 객체가 처음 활성화될 때, 남아있는 초기화 작업을 진행
/// </summary>
public class Monster : MonoBehaviour, IScriptableData
{
    protected MonsterStat_SO _monsterSO = null;
    protected DefaultStat _stat;
    protected Rigidbody2D _rigid;
    protected Animator _animator;
    protected SpriteRenderer _render;
    protected AudioSource _audioSource;
    protected Collider2D _col;

    protected const float SpawnRadius = 4f;

    protected float _health;
    protected float _maxHealth;
    protected float _user_Experience;
    protected int _inGameExperience;
    protected bool _canFlipX = true;
    protected bool _isVisible = false;

    private const float CollectDelay = 20;

    private Plane[] _planes = new Plane[6];
    private Coroutine _collect = null;
    private WaitForSeconds _waitCollect = new(CollectDelay);
    private bool _didInit = false;

    public ScriptableObject SO { set { _monsterSO = value as MonsterStat_SO; } }
    // OnEnable로 넘어가면서 초기화 작업을 하지 못하도록 비활성화
    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }
    // 활성화 되었을 때, 초기화가 진행되지 않았을 경우 초기화 작업을 진행
    protected virtual void OnEnable()
    {
        if(!_didInit)
        {
            Init();

            _didInit = true;
        }

        _canFlipX = true;
    }
    // 체력 재생
    private void Update()
    {
        _health = Mathf.Min(_health + _stat.healthRegenPerSec * Time.deltaTime, _maxHealth);
    }
    protected virtual void FixedUpdate()
    {
        IsInvisible();
    }
    // 현재 공격력 반환
    public float Damage()
    {
        return _stat.damage * Managers.Game.difficultyScaler.IncreaseStat;
    }
    // 몬스터 상태 초기화
    protected virtual void Set()
    {
        _maxHealth = _health = _stat.health * Managers.Game.difficultyScaler.IncreaseStat;
        _animator.speed = 1;
    }
    // 초기화
    protected virtual void Init()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        if(TryGetComponent(out Collider2D col))
        {
            _col = col;
        }
        else
        {
            _col = gameObject.AddComponent<BoxCollider2D>();
        }

        _stat = new(_monsterSO.Stat);
        _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigid.simulated = false;
        _render.enabled = false;
        _audioSource.playOnAwake = false;
        _user_Experience = _monsterSO.User_Experience;
        _inGameExperience = _monsterSO.InGame_Experience;
    }
    // 현재 객체가 카메라 영역 내에 있는지 검사, 영역 내에 없다면 보일 때까지 애니메이션을 재생하지 않음
    private void IsInvisible()
    {
        _planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(_planes, _col.bounds))
        {
            _animator.speed = 1;
            _isVisible = true;

            if(_collect != null)
            {
                StopCoroutine(_collect);

                _collect = null;
            }
        }
        else
        {
            _animator.speed = 0;
            _isVisible = false;

            if(_collect == null)
            {
                _collect = StartCoroutine(Collecting());
            }
        }
    }
    // 카메라 영역을 기준으로 객체가 활성화 될 때 초기 위치 설정
    protected virtual void SetPosition()
    {
        float randomValue = Random.Range(0, 360);
        float x = Mathf.Cos(randomValue) * (CameraUtil.CameraWidth / 2 + SpawnRadius);
        float y = Mathf.Sin(randomValue) * (CameraUtil.CameraHeight / 2 + SpawnRadius);

        transform.position = new Vector2(x, y) + (Vector2)Managers.Game.player.gameObject.transform.position + Managers.Game.player.move.Direction.normalized * 4;
    }
    // 위치를 기준으로 플레이어를 바라보는 방향으로 스프라이트를 플립
    protected virtual void FlipX()
    {
        if(!_canFlipX)
        {
            return;
        }

        if(transform.position.x != Managers.Game.player.transform.position.x)
        {
            _render.flipX = !(transform.position.x > Managers.Game.player.transform.position.x);
        }
    }
    // 카메라 영역에 CollectDelay초 동안 보이지 않는다면, 오브젝트 풀에서 회수
    private IEnumerator Collecting()
    {
        yield return _waitCollect;

        _collect = null;

        gameObject.SetActive(false);
    }
}
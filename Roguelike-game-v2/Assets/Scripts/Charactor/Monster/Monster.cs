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
    protected MonsterStat_SO monsterSO = null;
    protected DefaultStat stat;
    protected Rigidbody2D rigid;
    protected Animator animator;
    protected SpriteRenderer render;
    protected AudioSource audioSource;
    protected Collider2D col;

    protected const float spawnRadius = 4f;

    protected float health;
    protected float maxHealth;
    protected float user_Experience;
    protected int inGame_Experience;
    protected bool isVisible = false;

    private const float collectDelay = 20;

    private Plane[] planes = new Plane[6];
    private Coroutine collect = null;
    private WaitForSeconds waitCollect = new(collectDelay);
    private bool didInit = false;

    public ScriptableObject SO { set { monsterSO = value as MonsterStat_SO; } }
    // OnEnable로 넘어가면서 초기화 작업을 하지 못하도록 비활성화
    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }
    // 활성화 되었을 때, 초기화가 진행되지 않았을 경우 초기화 작업을 진행
    protected virtual void OnEnable()
    {
        if(!didInit)
        {
            Init();

            didInit = true;
        }
    }
    // 체력 재생
    private void Update()
    {
        health = Mathf.Min(health + stat.healthRegenPerSec * Time.deltaTime, maxHealth);
    }
    protected virtual void FixedUpdate()
    {
        IsInvisible();
    }
    // 현재 공격력 반환
    public float Damage()
    {
        return stat.damage * Managers.Game.difficultyScaler.IncreaseStat;
    }
    // 몬스터 상태 초기화
    protected virtual void Set()
    {
        maxHealth = health = stat.health * Managers.Game.difficultyScaler.IncreaseStat;
        animator.speed = 1;
    }
    // 초기화
    protected virtual void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if(TryGetComponent(out Collider2D col))
        {
            this.col = col;
        }
        else
        {
            this.col = gameObject.AddComponent<BoxCollider2D>();
        }

        stat = new(monsterSO.Stat);
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigid.simulated = false;
        render.enabled = false;
        audioSource.playOnAwake = false;
        user_Experience = monsterSO.User_Experience;
        inGame_Experience = monsterSO.InGame_Experience;
    }
    // 현재 객체가 카메라 영역 내에 있는지 검사, 영역 내에 없다면 보일 때까지 애니메이션을 재생하지 않음
    private void IsInvisible()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            animator.speed = 1;
            isVisible = true;

            if(collect != null)
            {
                StopCoroutine(collect);

                collect = null;
            }
        }
        else
        {
            animator.speed = 0;
            isVisible = false;

            if(collect == null)
            {
                collect = StartCoroutine(Collecting());
            }
        }
    }
    // 카메라 영역을 기준으로 객체가 활성화 될 때 초기 위치 설정
    protected virtual void SetPosition()
    {
        float randomValue = Random.Range(0, 360);
        float x = Mathf.Cos(randomValue) * (CameraUtil.CameraWidth / 2 + spawnRadius);
        float y = Mathf.Sin(randomValue) * (CameraUtil.CameraHeight / 2 + spawnRadius);

        transform.position = new Vector2(x, y) + (Vector2)Managers.Game.player.gameObject.transform.position + Managers.Game.player.move.Direction.normalized * 4;
    }
    // 위치를 기준으로 플레이어를 바라보는 방향으로 스프라이트를 플립
    protected virtual void FlipX()
    {
        if(transform.position.x != Managers.Game.player.transform.position.x)
        {
            render.flipX = !(transform.position.x > Managers.Game.player.transform.position.x);
        }
    }
    // 카메라 영역에 collectDelay초 동안 보이지 않는다면, 오브젝트 풀에서 회수
    private IEnumerator Collecting()
    {
        yield return waitCollect;

        collect = null;

        gameObject.SetActive(false);
    }
}
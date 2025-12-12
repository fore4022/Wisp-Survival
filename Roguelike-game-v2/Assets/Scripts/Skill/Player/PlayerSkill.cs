using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
/// <summary>
/// <para>
/// 플레이어 스킬에 대한 기본 구현
/// </para>
/// 생성 이후, 처음으로 사용되는 시점에 초기화
/// </summary>
public class PlayerSkill : MonoBehaviour, IScriptableData, IDamage
{
    [SerializeField] protected Collider2D _defaultCollider = null;

    [SerializeField] protected bool _playAnimationOnEnable = true;
    [SerializeField] protected bool _playColliderOnEnable = true;

    protected IPlayerSkill _skill;
    protected Skill_SO _so;
    protected SpriteRenderer _render;
    protected Animator _animator;
    protected AudioSource _audioSource;

    protected Coroutine _baseCast;
    protected int _level;

    private bool _isMaxLevel = false;

    public ScriptableObject SO { set { _so = value as Skill_SO; } }
    public float DamageAmount { get { return Managers.Game.player.Stat.damage * _so.DamageCoefficient[_level]; } }
    protected void Awake()
    {
        Init();

        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        StartCoroutine(CastSkill());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnEnter(collision.gameObject);
    }
    private void Init()
    {
        if(TryGetComponent(out IPlayerSkill skill))
        {
            this._skill = skill;
        }

        if(_defaultCollider == null)
        {
            _defaultCollider = GetComponent<Collider2D>();
        }

        _render = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        Set();
    }
    private void Set()
    {
        _defaultCollider.enabled = false;
        _render.enabled = false;
        _animator.speed = 0;
    }
    private void OnEnter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            _skill.Enter(go);
        }
    }
    private IEnumerator CastSkill()
    {
        _level = Managers.Game.inGameData_Manage.skill.GetLevel(_so.TypePath);

        if(_level == Skill_SO.MaxLevel - 1 && !_isMaxLevel)
        {
            _render.color = _so.MaxLevelColor;
            _isMaxLevel = true;
        }

        if(_so.FlipX)
        {
            if(Random.Range(0, 2) == 1)
            {
                _render.flipX = true;
            }
            else
            {
                _render.flipX = false;
            }
        }

        if(_so.FlipY)
        {
            if(Random.Range(0, 2) == 1)
            {
                _render.flipY = true;
            }
            else
            {
                _render.flipY = false;
            }
        }

        _skill.Set();

        _defaultCollider.enabled = _playColliderOnEnable;
        _render.enabled = true;
        _animator.speed = _playAnimationOnEnable ? 1 : 0;
        _baseCast = StartCoroutine(BaseCasting());

        yield return new WaitUntil(() => _baseCast == null);

        gameObject.SetActive(false);
    }
    private IEnumerator BaseCasting()
    {
        yield return new WaitUntil(() => _skill.Finished);

        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= GameUtil.AnimationEndTime);

        _baseCast = null;

        Set();
    }
}
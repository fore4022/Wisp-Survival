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
    [SerializeField] protected Collider2D defaultCollider = null;

    [SerializeField] protected bool playAnimationOnEnable = true;
    [SerializeField] protected bool playColliderOnEnable = true;

    protected IPlayerSkill skill;
    protected Skill_SO so;
    protected SpriteRenderer render;
    protected Animator animator;
    protected AudioSource audioSource;

    protected Coroutine baseCast;
    protected int level;

    private bool isMaxLevel = false;

    public ScriptableObject SO { set { so = value as Skill_SO; } }
    public float DamageAmount { get { return Managers.Game.player.Stat.damage * so.DamageCoefficient[level]; } }
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
            this.skill = skill;
        }

        if(defaultCollider == null)
        {
            defaultCollider = GetComponent<Collider2D>();
        }

        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Set();
    }
    private void Set()
    {
        defaultCollider.enabled = false;
        render.enabled = false;
        animator.speed = 0;
    }
    private void OnEnter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            skill.Enter(go);
        }
    }
    private IEnumerator CastSkill()
    {
        level = Managers.Game.inGameData_Manage.skill.GetLevel(so.TypePath);

        if(level == Skill_SO.maxLevel - 1 && !isMaxLevel)
        {
            render.color = so.MaxLevelColor;
            isMaxLevel = true;
        }

        if(so.FlipX)
        {
            if(Random.Range(0, 2) == 1)
            {
                render.flipX = true;
            }
            else
            {
                render.flipX = false;
            }
        }

        if(so.FlipY)
        {
            if(Random.Range(0, 2) == 1)
            {
                render.flipY = true;
            }
            else
            {
                render.flipY = false;
            }
        }

        skill.Set();

        defaultCollider.enabled = playColliderOnEnable;
        render.enabled = true;
        animator.speed = playAnimationOnEnable ? 1 : 0;
        baseCast = StartCoroutine(BaseCasting());

        yield return new WaitUntil(() => baseCast == null);

        gameObject.SetActive(false);
    }
    private IEnumerator BaseCasting()
    {
        yield return new WaitUntil(() => skill.Finished);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= GameUtil.animationEndTime);

        baseCast = null;

        Set();
    }
}
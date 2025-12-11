using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class MonsterSkill : MonoBehaviour
{
    [SerializeField][Min(0.01f)] protected float animationSpeed = 1;

    protected SpriteRenderer render;
    protected Animator animator;
    protected AudioSource audioSource;
    protected Rigidbody2D rigid;
    protected Collider2D col;

    protected bool isInit = false;

    private const float collectDelay = 3;

    private Coroutine collect;
    private WaitForSeconds delay = new(collectDelay);
    private Plane[] planes = new Plane[6];
    
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        if(!isInit)
        {
            Init();

            isInit = true;

            SetActive(false);
        }

        Enable();
    }
    protected void FixedUpdate()
    {
        IsInvisible();
    }
    protected virtual void Init()
    {
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        if(col == null)
        {
            col = GetComponent<Collider2D>();
        }

        rigid.gravityScale = 0;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    protected virtual void SetActive(bool isActive)
    {
        render.enabled = isActive;
        animator.speed = isActive ? animationSpeed : 0;
    }
    private void OnDisable()
    {
        if(isInit)
        {
            Disable();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnEnter(collision.gameObject);
    }
    protected virtual void Disable()
    {
        SetActive(false);
    }
    private void OnEnter(GameObject go)
    {
        if(go.CompareTag("Player"))
        {
            Enter(go);
        }
    }
    private void IsInvisible()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            if(collect != null)
            {
                StopCoroutine(collect);

                collect = null;
            }

            animator.speed = 1;
        }
        else
        {
            collect = StartCoroutine(Collecting());

            animator.speed = 0;
        }
    }
    private IEnumerator Collecting()
    {
        yield return delay;

        gameObject.SetActive(false);
    }
    protected virtual void Enter(GameObject go) { }
    protected abstract void Enable();
}
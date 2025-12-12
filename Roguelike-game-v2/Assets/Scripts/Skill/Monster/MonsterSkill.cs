using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class MonsterSkill : MonoBehaviour
{
    [SerializeField][Min(0.01f)] protected float _animationSpeed = 1;

    protected SpriteRenderer _render;
    protected Animator _animator;
    protected AudioSource _audioSource;
    protected Rigidbody2D _rigid;
    protected Collider2D _col;

    protected bool _isInit = false;

    private const float CollectDelay = 3;

    private Coroutine _collect;
    private WaitForSeconds _delay = new(CollectDelay);
    private Plane[] _planes = new Plane[6];
    
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        if(!_isInit)
        {
            Init();

            _isInit = true;

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
        _render = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();

        if(_col == null)
        {
            _col = GetComponent<Collider2D>();
        }

        _rigid.gravityScale = 0;
        _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    protected virtual void SetActive(bool isActive)
    {
        _render.enabled = isActive;
        _animator.speed = isActive ? _animationSpeed : 0;
    }
    private void OnDisable()
    {
        if(_isInit)
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
        _planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(_planes, _col.bounds))
        {
            if(_collect != null)
            {
                StopCoroutine(_collect);

                _collect = null;
            }

            _animator.speed = 1;
        }
        else
        {
            _collect = StartCoroutine(Collecting());

            _animator.speed = 0;
        }
    }
    private IEnumerator Collecting()
    {
        yield return _delay;

        gameObject.SetActive(false);
    }
    protected virtual void Enter(GameObject go) { }
    protected abstract void Enable();
}
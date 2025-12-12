using System.Collections;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MonsterSkill_B : MonsterSkill_Damage, IFakeShadowSource
{
    [SerializeField][Min(0.2f)] private float _duration = 0.5f;
    [SerializeField] private Vector3 _skillOffset;

    private const float PreActionDelay = 0.035f;

    private Color _defaultColor;
    private Vector3 _targetPosition;
    private Vector3 _initialPosition;
    private Vector3 _initialScale;
    private float _scaleValue;

    public SpriteRenderer SpriteRender { get { return _render; } }
    public Vector3 TargetPosition { get { return _targetPosition; } }
    public Vector3 InitialPosition { get { return _initialPosition; } }
    public Vector3 CurrentPosition { get { return transform.position; } }
    protected override void Enable()
    {
        _targetPosition = transform.position;
        _initialPosition = transform.position += _skillOffset;
        transform.localScale = _initialScale;
        _col.enabled = false;

        SetActive(true);
        StartCoroutine(Casting());
    }
    protected override void Init()
    {
        base.Init();

        _scaleValue = transform.localScale.x;
        _defaultColor = _render.color;
        _initialScale = new Vector2(_scaleValue, _scaleValue);
    }
    protected override void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        gameObject.SetActive(false);
    }
    protected override void Disable()
    {
        transform.Kill();

        base.Disable();
    }
    private IEnumerator Casting()
    {
        transform.SetScale(_scaleValue / 5 * 3, _duration)
            .SetPosition(_targetPosition, _duration, EaseType.InQuad);

        StartCoroutine(ColorUtil.ChangeColor(_render, Color.white, _defaultColor, _duration));

        yield return new WaitForSeconds(_duration - PreActionDelay * 2);

        _col.enabled = true;

        yield return new WaitForSeconds(PreActionDelay);

        gameObject.SetActive(false);
    }
}
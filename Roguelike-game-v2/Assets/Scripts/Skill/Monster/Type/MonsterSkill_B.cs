using System.Collections;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MonsterSkill_B : MonsterSkill_Damage, IFakeShadowSource
{
    [SerializeField][Min(0.2f)] private float duration = 0.5f;
    [SerializeField] private Vector3 skillOffset;

    private const float preActionDelay = 0.035f;

    private Color defaultColor;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private float scaleValue;

    public SpriteRenderer SpriteRender { get { return render; } }
    public Vector3 TargetPosition { get { return targetPosition; } }
    public Vector3 InitialPosition { get { return initialPosition; } }
    public Vector3 CurrentPosition { get { return transform.position; } }
    protected override void Enable()
    {
        targetPosition = transform.position;
        initialPosition = transform.position += skillOffset;
        transform.localScale = initialScale;
        col.enabled = false;

        SetActive(true);
        StartCoroutine(Casting());
    }
    protected override void Init()
    {
        base.Init();

        scaleValue = transform.localScale.x;
        defaultColor = render.color;
        initialScale = new Vector2(scaleValue, scaleValue);
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
        transform.SetScale(scaleValue / 5 * 3, duration)
            .SetPosition(targetPosition, duration, EaseType.InQuad);

        StartCoroutine(ColorUtil.ChangeColor(render, Color.white, defaultColor, duration));

        yield return new WaitForSeconds(duration - preActionDelay * 2);

        col.enabled = true;

        yield return new WaitForSeconds(preActionDelay);

        gameObject.SetActive(false);
    }
}
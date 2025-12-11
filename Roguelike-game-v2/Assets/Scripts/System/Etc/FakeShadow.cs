using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
/// <summary>
/// <para>
/// 그림자 렌더러
/// </para>
/// IFakeShadowSource를 가진 대상의 자식에 위치를 통해 그림자를 구현
/// </summary>
public class FakeShadow : MonoBehaviour
{
    private const float alphaMin = 155;
    private readonly float alphaRange = 255 - alphaMin;

    private IFakeShadowSource source = null;
    private SpriteRenderer render;

    private Sprite sprite;
    private Color alphaColor;
    private Vector3 vec = default;
    private float value;

    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        StartCoroutine(AnimatedShadowDrop());
    }
    private void Init()
    {
        render = GetComponent<SpriteRenderer>();

        if(transform.parent != null)
        {
            transform.parent.TryGetComponent(out source);

            if(source == null)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void AdjustmentPosition()
    {
        vec = new Vector3(0, sprite.rect.height / sprite.pixelsPerUnit) * transform.parent.localScale.x;
        transform.position = source.CurrentPosition - vec * (1 - value);
    }
    private void AdjustmentScale()
    {
        transform.localScale = new Vector3(0.65f + value / 2, 0.65f + value / 2);
    }
    private void AdjustmentAlpha()
    {
        alphaColor = render.color;
        alphaColor.a = ((alphaMin + alphaRange * value) / 255);
        render.color = alphaColor;
    }
    private void Factor()
    {
        value = Mathf.InverseLerp(source.InitialPosition.y, source.TargetPosition.y, source.CurrentPosition.y);
    }
    private IEnumerator AnimatedShadowDrop()
    {
        while(true)
        {
            if(render.sprite != source.SpriteRender.sprite)
            {
                sprite = render.sprite = source.SpriteRender.sprite;
            }

            Factor();
            AdjustmentPosition();
            AdjustmentScale();
            AdjustmentAlpha();

            yield return null;
        }
    }
}
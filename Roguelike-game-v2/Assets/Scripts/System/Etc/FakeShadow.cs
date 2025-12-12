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
    private const float _alphaMin = 155;
    private readonly float _alphaRange = 255 - _alphaMin;

    private IFakeShadowSource _source = null;
    private SpriteRenderer _render;

    private Sprite _sprite;
    private Color _alphaColor;
    private Vector3 _vec = default;
    private float _value;

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
        _render = GetComponent<SpriteRenderer>();

        if(transform.parent != null)
        {
            transform.parent.TryGetComponent(out _source);

            if(_source == null)
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
        _vec = new Vector3(0, _sprite.rect.height / _sprite.pixelsPerUnit) * transform.parent.localScale.x;
        transform.position = _source.CurrentPosition - _vec * (1 - _value);
    }
    private void AdjustmentScale()
    {
        transform.localScale = new Vector3(0.65f + _value / 2, 0.65f + _value / 2);
    }
    private void AdjustmentAlpha()
    {
        _alphaColor = _render.color;
        _alphaColor.a = ((_alphaMin + _alphaRange * _value) / 255);
        _render.color = _alphaColor;
    }
    private void Factor()
    {
        _value = Mathf.InverseLerp(_source.InitialPosition.y, _source.TargetPosition.y, _source.CurrentPosition.y);
    }
    private IEnumerator AnimatedShadowDrop()
    {
        while(true)
        {
            if(_render.sprite != _source.SpriteRender.sprite)
            {
                _sprite = _render.sprite = _source.SpriteRender.sprite;
            }

            Factor();
            AdjustmentPosition();
            AdjustmentScale();
            AdjustmentAlpha();

            yield return null;
        }
    }
}
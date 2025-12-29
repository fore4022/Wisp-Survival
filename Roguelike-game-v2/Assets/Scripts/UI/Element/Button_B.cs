using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
public abstract class Button_B : Button_Default, IPointerDownHandler, IPointerExitHandler
{
    protected Coroutine _adjustmentScale = null;
    protected float _minScale = 1;
    protected float _maxScale = 1.025f;
    protected float _duration = 0.15f;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        DOTween.Complete(_rectTransform);
        _rectTransform.DOScale(_maxScale, _duration);
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Complete(_rectTransform);
        _rectTransform.DOScale(_minScale, _duration);
    }
}
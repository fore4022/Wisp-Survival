using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class Button_A : Button_Default, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    protected float _minScale = 1f;
    protected float _maxScale = 1.075f;
    protected float _minAlpha = 205f;
    protected float _maxAlpha = 255f;
    protected float _duration = 0.1f;

    private Image _image;

    private bool _isPointerDown = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp();
    }
    protected virtual void PointerEnter()
    {
        DOTween.Complete(_rectTransform);
        _rectTransform.DOScale(_maxScale, _duration);
    }
    protected virtual void PointerExit()
    {
        if(_isPointerDown) { return; }

        DOTween.Complete(_rectTransform);
        _rectTransform.DOScale(_minScale, _duration);
    }
    protected virtual void PointerDown()
    {
        _isPointerDown = true;
    }
    protected virtual void PointerUp()
    {
        _rectTransform.localScale = new(_minScale, _minScale);

        _isPointerDown = false;
    }
    private void Set()
    {
        UIElementUtility.SetImageAlpha(_image, _maxAlpha, _duration);
    }
    protected override void Init()
    {
        _image = GetComponent<Image>();

        base.Init();
        Set();
    }
}
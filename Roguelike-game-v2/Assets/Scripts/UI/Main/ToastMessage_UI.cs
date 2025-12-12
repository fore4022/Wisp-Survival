using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ToastMessage_UI : UserInterface
{
    private Image _img;
    private TextMeshProUGUI _toast;

    private Coroutine _coroutineImg = null;
    private Coroutine _coroutineText = null;
    private Coroutine _coroutine = null;

    private const float Delay = 1.25f;

    public override void SetUserInterface()
    {
        _img = GetComponent<Image>();
        _toast = transform.GetComponentInChild<TextMeshProUGUI>();

        Managers.UI.Hide<ToastMessage_UI>();
    }
    protected override void Enable()
    {
        _coroutine = StartCoroutine(ToastHide());
    }
    public void SetText(string text)
    {
        _toast.text = text;
    }
    private void OnDisable()
    {
        if(_coroutine != null)
        {
            if(_coroutineImg != null)
            {
                CoroutineHelper.Stop(_coroutineImg);
                CoroutineHelper.Stop(_coroutineText);
            }

            _coroutine = null;
        }
    }
    private IEnumerator ToastHide()
    {
        yield return new WaitUntil(() => _toast.text != "");

        UIElementUtility.SetImageAlpha(_img, 50);
        UIElementUtility.SetTextAlpha(_toast, 255);

        yield return new WaitForSeconds(Delay);

        _coroutineImg = UIElementUtility.SetImageAlpha(_img, 0, Delay);
        _coroutineText = UIElementUtility.SetTextAlpha(_toast, 0, Delay);

        yield return new WaitForSeconds(Delay + 0.5f);

        _coroutine = null;
        _toast.text = "";

        Managers.UI.Hide<ToastMessage_UI>();
    }
}
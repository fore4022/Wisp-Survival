using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LoadingOverlay_UI : UserInterface
{
    private const float LimitTime = 0.5f;

    private Image _background;

    private const float MinAlpha = 0;
    private const float MaxAlpha = 255;

    private bool _isFadeIn = true;
    private bool _isFadeOut = false;

    public bool IsFadeIn { get { return _isFadeIn; } }
    public override void SetUserInterface()
    {
        _background = transform.GetComponentInChild<Image>();

        transform.SetParent(null, false);
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Effecting());
    }
    public void FadeOut()
    {
        _isFadeOut = true;
    }
    private IEnumerator Effecting()
    {
        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        UIElementUtility.SetImageAlpha(_background, MaxAlpha, LimitTime, false);

        yield return new WaitForSecondsRealtime(LimitTime);

        _isFadeIn = false;

        yield return new WaitUntil(() => _isFadeOut);

        UIElementUtility.SetImageAlpha(_background, MinAlpha, LimitTime);

        yield return new WaitForSecondsRealtime(LimitTime);

        Managers.UI.Destroy<LoadingOverlay_UI>();
    }
}
using System.Collections;
using TMPro;
using UnityEngine;
public class StartMessage_UI : UserInterface
{
    private TextMeshProUGUI _tmp;

    private const float Duration = 1.2f;

    private Coroutine _textAnimation;
    private Coroutine _blink;
    private WaitForSeconds _delay;
    private int _state = 0;

    public override void SetUserInterface()
    {
        _tmp = GetComponent<TextMeshProUGUI>();

        _textAnimation = StartCoroutine(Effecting());
    }
    public void SetState()
    {
        _state++;

        if(_state == 1)
        {
            Blink();
        }
        else if(_state == 2)
        {
            StopCoroutine(_textAnimation);

            _textAnimation = StartCoroutine(Effecting());
        }
    }
    private IEnumerator Effecting()
    {
        string text;
        int count = 0;
        int i;

        if(_state == 0)
        {
            text = "Loading";
        }
        else
        {
            text = "Starting";

            StopCoroutine(_blink);
            UIElementUtility.SetTextAlpha(_tmp, 255, 0);
        }

        _delay = new(Duration / 2);

        while(true)
        {
            _tmp.text = text;

            for(i = 0; i < count; i++)
            {
                _tmp.text += ".";
            }

            yield return _delay;

            count++;

            if(count > 3)
            {
                count = 0;
            }
        }
    }
    private void Blink()
    {
        StopCoroutine(_textAnimation);

        _tmp.text = "PRESS TO START";
        _blink = StartCoroutine(UIElementUtility.BlinkText(_tmp, Duration, false));
    }
}
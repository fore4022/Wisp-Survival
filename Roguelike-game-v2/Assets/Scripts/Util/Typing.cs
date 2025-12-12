using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
/// <summary>
/// <para>
/// 타이핑 및 삭제 효과 제공
/// </para>
/// Text에 대한 효과는 구현되지 않음
/// </summary>
public static class Typing
{
    private static WaitForSecondsRealtime _waitRealSec = new(0.03f);

    // 타이핑 실행 및 소요 시간 반환
    public static WaitForSecondsRealtime EffectAndGetWaiting(TextMeshProUGUI tmp, string str, float delay = 0, bool recursive = false, string currentStr = "")
    {
        CoroutineHelper.Start(TypeEffecting(tmp, str, recursive, currentStr), CoroutineType.UserInterface);

        return new(_waitRealSec.waitTime * str.Length + delay);
    }
    // 타이핑 실행 및 코루틴과 소요 시간 반환
    public static (Coroutine, WaitForSecondsRealtime) GetEffectAndWaiting(TextMeshProUGUI tmp, string str, float delay = 0, bool recursive = false, string currentStr = "")
    {
        Coroutine coroutine = CoroutineHelper.Start(TypeEffecting(tmp, str, recursive, currentStr), CoroutineType.UserInterface);

        return (coroutine, new(_waitRealSec.waitTime * str.Length + delay));
    }
    // 타이핑 효과, 하위 텍스트까지 적용 가능, 기존 문자열에 더해서 사용 가능
    public static IEnumerator TypeEffecting(TextMeshProUGUI tmp, string str, bool recursive = false, string currentStr = "")
    {
        StringBuilder builder = new();
        string text;

        if(currentStr == "")
        {
            builder.Append(tmp.text);
        }
        else
        {
            builder.Append(currentStr);
        }

        if(recursive)
        {
            TextMeshProUGUI[] tmpArray = tmp.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

            for(int i = 0; i < str.Length; i++)
            {
                yield return _waitRealSec;

                builder.Append(str[i]);

                text = builder.ToString();

                foreach (TextMeshProUGUI _tmp in tmpArray)
                {
                    _tmp.text = text;
                }
            }
        }
        else
        {
            for(int i = 0; i < str.Length; i++)
            {
                yield return _waitRealSec;

                builder.Append(str[i]);

                tmp.text = builder.ToString();
            }
        }
    }
    // 전체 텍스트 제거 효과
    public static IEnumerator EraseEffecting(TextMeshProUGUI tmp, float duration)
    {
        WaitForSecondsRealtime waitRealSec;
        StringBuilder builder = new();
        int count = 0;

        builder.Append(tmp.text);

        for(int i = 0; i < builder.Length; i++)
        {
            if(builder[i] != '\r' || builder[i] != '\n')
            {
                count++;
            }
        }

        waitRealSec = new(duration / count);

        while(builder.Length > 0)
        {
            yield return waitRealSec;

            IsNewLineStart(ref builder);

            builder.Remove(0, 1);

            tmp.text = builder.ToString();
        }
    }
    // 지정한 개수의 텍스트 제거 효과
    public static IEnumerator EraseEffecting(TextMeshProUGUI tmp, int targetCount = 0)
    {
        StringBuilder builder = new();

        builder.Append(tmp.text);

        while(builder.Length > targetCount)
        {
            yield return _waitRealSec;

            IsNewLineStart(ref builder);

            builder.Remove(0, 1);

            tmp.text = builder.ToString();
        }
    }
    // 개행 문자 제거, 텍스트 제거 효과의 문자열이 의도대로 실행되기 위해서 사용
    private static void IsNewLineStart(ref StringBuilder builder)
    {
        if(builder[0] == '\r' || builder[0] == '\n')
        {
            builder.Remove(0, 1);

            IsNewLineStart(ref builder);
        }
    }
}
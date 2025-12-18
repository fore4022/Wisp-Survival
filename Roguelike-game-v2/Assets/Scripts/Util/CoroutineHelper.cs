using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// Non-MonoBehaviour 클래스에서도 코루틴을 안전하게 실행·정지할 수 있도록 지원하는 기능을 구현
/// </para>
/// CoroutineHelper로 실행된 코루틴은 CoroutineHelper를 통해서 정지 가능함, CoroutineType으로 코루틴을 구분해서 제어 가능
/// </summary>
public static class CoroutineHelper
{
    private static Manage_Mono _manageMono = null;
    private static UserInterface_Mono _userInterfaceMono = null;
    private static InGameSystem_Mono _inGameSystemMono = null;
    private static Etc_Mono _etcMono = null;

    // 코루틴 실행
    public static Coroutine Start(IEnumerator coroutine, CoroutineType type = CoroutineType.Etc)
    {
        return GetMonoBehaviour(type).StartCoroutine(coroutine);
    }
    // 코루틴 정지, 코루틴을 실행시킨 MonoScript를 통해서만 코루틴을 정지할 수 있다.
    public static void Stop(Coroutine coroutine, CoroutineType type = CoroutineType.Etc)
    {
        GetMonoBehaviour(type).StopCoroutine(coroutine);
    }
    // MonoScript의 모든 코루틴 정지
    public static void StopAllCoroutine(CoroutineType type)
    {
        GetMonoBehaviour(type).StopAllCoroutines();
    }
    // Type에 해당하는 MonoScript를 반환, MonoScript가 Null인 경우에 빈 객체에 MonoScript들을 생성 후 반환
    private static MonoBehaviour GetMonoBehaviour(CoroutineType type = CoroutineType.Etc)
    {
        if(_manageMono == null)
        {
            GameObject go = new GameObject("@MonoScript");

            _manageMono = go.AddComponent<Manage_Mono>();
            _userInterfaceMono = go.AddComponent<UserInterface_Mono>();
            _inGameSystemMono = go.AddComponent<InGameSystem_Mono>();
            _etcMono = go.AddComponent<Etc_Mono>();

            Object.DontDestroyOnLoad(go);
        }

        switch(type)
        {
            case CoroutineType.Manage:
                return _manageMono;
            case CoroutineType.UserInterface:
                return _userInterfaceMono;
            case CoroutineType.InGameSystem:
                return _inGameSystemMono;
        }

        return _etcMono;
    }
}
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// <para>
/// 씬 전환과 로딩을 관리한다.
/// </para>
/// 씬 로드와 로딩 완료 이벤트, SceneLoading_UI 제어
/// </summary>
public class Scene_Manager
{
    public Action onLoad = null;
    public Action loadComplete = null;

    private string _sceneName = "Title";
    private bool _hasInitialization;
    private bool _isSceneLoading;

    public string CurrentSceneName { get { return _sceneName; } }
    public bool IsSceneLoading { get { return _isSceneLoading; } }
    // 이벤트 호출 및 기존 씬의 UI 정보 해제
    public void LoadScene(SceneNames sceneName, bool hasInitialization = true)
    {
        _sceneName = sceneName.ToString();
        _hasInitialization = hasInitialization;
        _isSceneLoading = true;

        onLoad?.Invoke();

        DOTween.KillAll();
        Managers.UI.Clear();
        Managers.UI.Show<LoadingOverlay_UI>();
        CoroutineHelper.Start(Managers.Scene.SceneSetting(), CoroutineType.Manage);
    }
    // 초기화 작업이 있는 씬에서는 직접 씬 로드를 완료
    public void LoadComplete()
    {
        loadComplete?.Invoke();

        Managers.UI.Get<LoadingOverlay_UI>().FadeOut();
    }
    // 이벤트 호출 및 새로운 씬 로드 대기, Fade Out
    public IEnumerator SceneSetting()
    {
        yield return new WaitUntil(() => Managers.UI.Get<LoadingOverlay_UI>() != null);

        yield return new WaitUntil(() => !Managers.UI.Get<LoadingOverlay_UI>().IsFadeIn);

        AddressableHelper.LoadingScene(_sceneName.ToString());

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == _sceneName);

        _isSceneLoading = false;

        if (!_hasInitialization)
        {
            LoadComplete();
        }
    }
}
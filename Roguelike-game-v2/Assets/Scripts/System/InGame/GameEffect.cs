using DG.Tweening;
using System.Collections;
using UnityEngine;
/// <summary>
/// GameOver ø¨√‚
/// </summary>
public class GameEffect
{
    private readonly WaitForSeconds _delay = new(Duration);
    private const float Duration = 0.4f;

    private float MaxOrthographicSize { get { return 6 * CameraSizeScale.orthographicSizeScale; } }
    private float MinOrthographicSize { get { return 1.25f * CameraSizeScale.orthographicSizeScale; } }
    public void StageFailed()
    {
        Managers.UI.Hide<LevelUp_UI>();

        CoroutineHelper.Start(GameOver_Effecting(), CoroutineType.Etc);
    }
    public void StageClear()
    {
        CoroutineHelper.Start(StageClear_Effecting(), CoroutineType.InGameSystem);
    }
    public void ContinuePlay()
    {
        CoroutineHelper.Start(ContinuePlay_Effecting(), CoroutineType.InGameSystem);
    }
    private IEnumerator GameOver_Effecting()
    {
        float totalTime = 0;
        float currentCameraSize = Camera.main.orthographicSize;

        while(totalTime != Duration)
        {
            totalTime += Time.unscaledDeltaTime;

            if(totalTime > Duration)
            {
                totalTime = Duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(currentCameraSize, 1.25f * CameraSizeScale.orthographicSizeScale, totalTime / Duration);

            yield return null;
        }

        Managers.UI.Show<GameOver_UI>();
    }
    private IEnumerator StageClear_Effecting()
    {
        Transform cam = Camera.main.transform;

        float totalTime = 0;

        cam.DOMove(cam.position + new Vector3(0, -0.3f), Duration);

        while(totalTime != Duration)
        {
            totalTime += Time.deltaTime;

            if(totalTime >= Duration)
            {
                totalTime = Duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(MaxOrthographicSize, MinOrthographicSize, totalTime / Duration);

            yield return null;
        }

        yield return _delay;

        Managers.UI.Show<GameOver_UI>();
    }
    private IEnumerator ContinuePlay_Effecting()
    {
        Transform cam = Camera.main.transform;

        float totalTime = 0;

        cam.DOMove(cam.position + new Vector3(0, 0.3f), Duration);

        while (totalTime != Duration)
        {
            totalTime += Time.deltaTime;

            if (totalTime >= Duration)
            {
                totalTime = Duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(MinOrthographicSize, MaxOrthographicSize, totalTime / Duration);

            yield return null;
        }

        InputManage.EnableInputAction<TouchControls>();
        Managers.UI.Show<HpSlider_UI>();

        if(Managers.Game.inGameData_Manage.player.LevelUpCount > 0)
        {
            Managers.UI.Show<LevelUp_UI>();
        }
    }
}
using System.Collections;
using UnityEngine;
/// <summary>
/// GameOver ø¨√‚
/// </summary>
public class GameEffect
{
    private readonly WaitForSeconds delay = new(duration);
    private const float duration = 0.4f;

    private float MaxOrthographicSize { get { return 6 * Camera_SizeScale.orthographicSizeScale; } }
    private float MinOrthographicSize { get { return 1.25f * Camera_SizeScale.orthographicSizeScale; } }
    public void StageFailed()
    {
        Managers.UI.Hide<LevelUp_UI>();

        CoroutineHelper.Start(GameOver_Effecting());
    }
    public void StageClear()
    {
        CoroutineHelper.Start(StageClear_Effecting());
    }
    public void ContinuePlay()
    {
        CoroutineHelper.Start(ContinuePlay_Effecting());
    }
    private IEnumerator GameOver_Effecting()
    {
        float totalTime = 0;
        float currentCameraSize = Camera.main.orthographicSize;

        while(totalTime != duration)
        {
            totalTime += Time.unscaledDeltaTime;

            if(totalTime > duration)
            {
                totalTime = duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(currentCameraSize, 1.25f * Camera_SizeScale.orthographicSizeScale, totalTime / duration);

            yield return null;
        }

        Managers.Game.player.Kill();
        Managers.UI.Show<GameOver_UI>();
    }
    private IEnumerator StageClear_Effecting()
    {
        Transform cam = Camera.main.transform;

        float totalTime = 0;

        cam.SetPosition(cam.position + new Vector3(0, -0.3f), duration);

        while(totalTime != duration)
        {
            totalTime += Time.deltaTime;

            if(totalTime >= duration)
            {
                totalTime = duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(MaxOrthographicSize, MinOrthographicSize, totalTime / duration);

            yield return null;
        }

        yield return delay;

        Managers.UI.Show<GameOver_UI>();
    }
    private IEnumerator ContinuePlay_Effecting()
    {
        Transform cam = Camera.main.transform;

        float totalTime = 0;

        cam.SetPosition(cam.position + new Vector3(0, 0.3f), duration);

        while (totalTime != duration)
        {
            totalTime += Time.deltaTime;

            if (totalTime >= duration)
            {
                totalTime = duration;
            }

            Camera.main.orthographicSize = Mathf.Lerp(MinOrthographicSize, MaxOrthographicSize, totalTime / duration);

            yield return null;
        }

        Input_Manage.EnableInputAction<TouchControls>();
        Managers.UI.Show<HpSlider_UI>();

        if(Managers.Game.inGameData_Manage.player.LevelUpCount > 0)
        {
            Managers.UI.Show<LevelUp_UI>();
        }
    }
}
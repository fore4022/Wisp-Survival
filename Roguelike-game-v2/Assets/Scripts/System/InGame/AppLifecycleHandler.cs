using UnityEngine;
/// <summary>
/// 게임 포커스 전환 처리
/// </summary>
public class AppLifecycleHandler : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        Managers.Data.Save();
    }
    private void OnApplicationFocus(bool focus)
    {
        if(Managers.Scene.CurrentSceneName.Equals(SceneNames.InGame.ToString()))
        {
            if(!Managers.Game.GameOver)
            {
                if(focus)
                {
                    Managers.Data.Save();
                }
                else
                {
                    if(Managers.Game.Playing)
                    {
                        Time.timeScale = 0;

                        Managers.UI.Show<PauseMenu_UI>();
                    }
                }
            }
        }
    }
}
using UnityEngine;
public class Pause_Button : Button_A
{
    protected override void PointerClick()
    {
        if(!Managers.UI.Get<LevelUp_UI>().gameObject.activeSelf)
        {
            Time.timeScale = 0;

            Managers.UI.Show<PauseMenu_UI>();
        }
    }
    protected override void Init()
    {
        _maxScale = 1.035f;

        base.Init();
    }
}
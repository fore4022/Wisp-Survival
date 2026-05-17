using UnityEngine;
public class Resume_UI : ButtonA
{
    protected override void PointerClick()
    {
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Hide<PauseMenu_UI>();
    }
}
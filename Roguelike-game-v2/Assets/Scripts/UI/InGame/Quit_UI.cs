public class Quit_UI : ButtonA
{
    protected override void PointerClick()
    {
        Managers.UI.Show<StageExitConfirm_UI>();
    }
}
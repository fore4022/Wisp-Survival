public class StatUpgrade_Button : ButtonA
{
    protected override void PointerClick()
    {
        Managers.UI.Get<StatUpgrade_UI>().ToggleUI();
    }
}
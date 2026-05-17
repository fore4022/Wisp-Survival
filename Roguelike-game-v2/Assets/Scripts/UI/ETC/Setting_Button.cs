public class Setting_Button : ButtonA
{
    protected override void PointerClick()
    {
        Managers.UI.Show<Setting_UI>();
    }
}
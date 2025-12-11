using UnityEngine;
public class NextStage_Button : Button_Default
{
    [SerializeField] private int sign;

    protected override void PointerClick()
    {
        Managers.UI.Get<StageIcon_UI>().UpdateUI(sign);
        Managers.UI.Get<StageInformation_UI>().Set();
    }
}
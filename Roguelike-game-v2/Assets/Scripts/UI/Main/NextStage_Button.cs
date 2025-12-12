using UnityEngine;
public class NextStage_Button : Button_Default
{
    [SerializeField] private int _sign;

    protected override void PointerClick()
    {
        Managers.UI.Get<StageIcon_UI>().UpdateUI(_sign);
        Managers.UI.Get<StageInformation_UI>().Set();
    }
}
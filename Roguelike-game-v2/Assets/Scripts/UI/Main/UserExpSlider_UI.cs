using TMPro;
using UnityEngine.UI;
public class UserExpSlider_UI : UserInterface
{
    private Slider _expSlider;
    private TextMeshProUGUI _expText;

    public override void SetUserInterface()
    {
        _expSlider = GetComponent<Slider>();
        _expText = transform.GetComponentInChild<TextMeshProUGUI>();

        UpdateExp();
    }
    public void UpdateExp()
    {
        if(Managers.Data.user.Level != GameUtil.MaxLevel)
        {
            _expSlider.value = (float)Managers.Data.user.Exp / Managers.Data.UserExpTable.RequiredEXP[Managers.Data.user.Level - 1];
            _expText.text = $"{Managers.Data.user.Exp:N0} / {Managers.Data.UserExpTable.RequiredEXP[Managers.Data.user.Level - 1]:N0}";
        }
        else
        {
            _expText.text = $"{Managers.Data.user.Exp:N0}";
        }
    }
}
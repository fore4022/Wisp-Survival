using TMPro;
public class UserLevelText_UI : UserInterface
{
    private TextMeshProUGUI _levelText;

    public override void SetUserInterface()
    {
        _levelText = GetComponent<TextMeshProUGUI>();

        LevelUpdate();
    }
    public void LevelUpdate()
    {
        if(Managers.Data.user.Level != GameUtil.MaxLevel)
        {
            _levelText.text = $"Lv. {Managers.Data.user.Level}";
        }
        else
        {
            _levelText.text = "Lv. MAX";
        }
    }
}
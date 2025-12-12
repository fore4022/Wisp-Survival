using TMPro;
public class SkillPoints_UI : UserInterface
{
    private TextMeshProUGUI _skillPoints;

    public override void SetUserInterface()
    {
        _skillPoints = GetComponent<TextMeshProUGUI>();

        Managers.UI.Hide<SkillPoints_UI>();
    }
    protected override void Enable()
    {
        SkillPointsUpdate();
    }
    public void SkillPointsUpdate()
    {
        _skillPoints.text = $"Skill Points : {Managers.Game.inGameData_Manage.player.LevelUpCount}";
    }
}
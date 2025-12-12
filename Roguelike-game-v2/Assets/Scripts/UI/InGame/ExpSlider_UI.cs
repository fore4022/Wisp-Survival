using UnityEngine.UI;
public class ExpSlider_UI : UserInterface
{
    private Slider _expSlider;

    public override void SetUserInterface()
    {
        _expSlider = GetComponent<Slider>();

        Init();
    }
    private void Init()
    {
        Managers.Game.inGameData_Manage.player.levelUpdate += MaxValueUpdate;
        Managers.Game.inGameData_Manage.player.experienceUpdate += ValueUpdate;

        MaxValueUpdate();
        ValueUpdate();
    }
    private void MaxValueUpdate()
    {
        _expSlider.maxValue = Managers.Game.inGameData_Manage.player.ExperienceForLevelUp;
    }
    private void ValueUpdate()
    {
        _expSlider.value = Managers.Game.inGameData_Manage.player.Experience;
    }
}
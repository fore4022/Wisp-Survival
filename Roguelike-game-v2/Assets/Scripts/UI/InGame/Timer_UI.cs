using TMPro;
/// <summary>
/// <para>
/// 인게임 타이머 구현
/// </para>
/// InGameTimer의 timerUpdate에 등록되어있다.
/// </summary>
public class Timer_UI : UserInterface
{
    private TextMeshProUGUI _timer;

    public override void SetUserInterface()
    {
        _timer = GetComponent<TextMeshProUGUI>();

        Managers.Game.inGameTimer.timerUpdate += TimerUpdate;
    }
    private void TimerUpdate()
    {
        if(Managers.Game.Playing)
        {
            if(Managers.Game.inGameTimer.GetHours == 0)
            {
                _timer.text = $"{Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
            }
            else
            {
                _timer.text = $"{Managers.Game.inGameTimer.GetHours:D2} : {Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
            }
        }
    }
}
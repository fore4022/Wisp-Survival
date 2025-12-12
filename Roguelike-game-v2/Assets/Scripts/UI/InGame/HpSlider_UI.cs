using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HpSlider_UI : UserInterface
{
    private Slider _hpSlider;

    public override void SetUserInterface()
    {
        _hpSlider = GetComponent<Slider>();

        CoroutineHelper.Start(WaitPlayerStatInit(), CoroutineType.UserInterface);
    }
    private void Init()
    {
        Managers.Game.player.maxHealthUpdate += MaxValueUpdate;
        Managers.Game.player.healthUpdate += ValueUpdate;

        MaxValueUpdate();
        ValueUpdate();
    }
    private void MaxValueUpdate()
    {
        _hpSlider.maxValue = Managers.Game.player.MaxHealth;
    }
    private void ValueUpdate()
    {
        _hpSlider.value = Managers.Game.player.Health;
    }
    private IEnumerator WaitPlayerStatInit()
    {
        yield return new WaitUntil(() => Managers.Game.player != null);

        Init();
    }
}
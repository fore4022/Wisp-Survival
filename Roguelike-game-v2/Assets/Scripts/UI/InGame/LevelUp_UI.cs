using System.Collections;
using UnityEngine;
using TMPro;
public class LevelUp_UI : UserInterface
{
    private TextMeshProUGUI _level;
    private Animator _animator;

    public override void SetUserInterface()
    {
        _level = transform.GetComponentInChild<TextMeshProUGUI>(true);
        _animator = transform.GetComponentInChild<Animator>(true);

        gameObject.SetActive(false);
    }
    protected override void Enable()
    {
        if(ShouldShowSkillSelection())
        {
            Time.timeScale = 0;
            Managers.Game.Playing = false;
        }

        StartCoroutine(AnimationPlaying());
    }
    private void Update()
    {
        _level.text = $"Lv.{Managers.Game.inGameData_Manage.player.Level}";
    }
    private bool ShouldShowSkillSelection()
    {
        return (Managers.Game.inGameData_Manage.player.MaxLevel >= Managers.Game.inGameData_Manage.player.Level) || Managers.Game.inGameData_Manage.player.LevelUpCount != 0;
    }    
    private IEnumerator AnimationPlaying()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        Input_Manage.DisableInputAction<TouchControls>();
        Managers.UI.Hide<CharactorController_UI>();

        if(ShouldShowSkillSelection())
        {
            Managers.UI.Show<SkillSelection_UI>();
        }

        Managers.UI.Hide<LevelUp_UI>();
    }
}
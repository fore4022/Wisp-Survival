using TMPro;
using UnityEngine;
public class StageExitConfirm_UI : UserInterface
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _giveUpStage_Sound;
    [SerializeField] private AudioClip _cancelStageExit_Sound;
    [SerializeField] private TextMeshProUGUI _tmp;

    private const string ExitAfterClearMessage = "스테이지가 클리어 되었습니다.";
    private const string ExitWithoutClearWarning = "스테이지가 클리어하지 못했습니다.\n경험치를 휙득할 수 없습니다.";

    public override void SetUserInterface()
    {
        Managers.UI.Hide<StageExitConfirm_UI>();
    }
    protected override void Enable()
    {
        if(Managers.Game.IsStageClear)
        {
            _tmp.text = ExitAfterClearMessage;
        }
        else
        {
            _tmp.text = ExitWithoutClearWarning;
        }

        Managers.UI.Get<PauseMenu_UI>().HideIcons();
    }
    public void OnGiveUpStage()
    {
        Managers.Data.user.Exp += Managers.Game.UserExp;
        _audioSource.clip = _giveUpStage_Sound;

        _audioSource.Play();
        Managers.UI.Hide<HeadUpDisplay_UI>();
        Managers.Game.Clear();
        Managers.Scene.LoadScene(SceneNames.Main, false);
    }
    public void OnCancelStageExit()
    {
        _audioSource.clip = _cancelStageExit_Sound;

        _audioSource.Play();
        Managers.UI.Get<PauseMenu_UI>().ShowIcons();
        Managers.UI.Hide<StageExitConfirm_UI>();
    }
}
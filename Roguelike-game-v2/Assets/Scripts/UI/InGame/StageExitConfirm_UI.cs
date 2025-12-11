using TMPro;
using UnityEngine;
public class StageExitConfirm_UI : UserInterface
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip giveUpStage_Sound;
    [SerializeField] private AudioClip cancelStageExit_Sound;
    [SerializeField] private TextMeshProUGUI tmp;

    private const string exitAfterClearMessage = "스테이지가 클리어 되었습니다.";
    private const string exitWithoutClearWarning = "스테이지가 클리어하지 못했습니다.\n경험치를 휙득할 수 없습니다.";

    public override void SetUserInterface()
    {
        Managers.UI.Hide<StageExitConfirm_UI>();
    }
    protected override void Enable()
    {
        if(Managers.Game.IsStageClear)
        {
            tmp.text = exitAfterClearMessage;
        }
        else
        {
            tmp.text = exitWithoutClearWarning;
        }

        Managers.UI.Get<PauseMenu_UI>().HideIcons();
    }
    public void OnGiveUpStage()
    {
        Managers.Data.user.Exp += Managers.Game.UserExp;
        audioSource.clip = giveUpStage_Sound;

        audioSource.Play();
        Managers.UI.Hide<HeadUpDisplay_UI>();
        Managers.Game.Clear();
        Managers.Scene.LoadScene(SceneNames.Main, false);
    }
    public void OnCancelStageExit()
    {
        audioSource.clip = cancelStageExit_Sound;

        audioSource.Play();
        Managers.UI.Get<PauseMenu_UI>().ShowIcons();
        Managers.UI.Hide<StageExitConfirm_UI>();
    }
}
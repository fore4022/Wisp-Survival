using UnityEngine;
public class Start_UI : ButtonDefault
{
    [SerializeField] private AudioClip _actionAvailableSound;
    [SerializeField] private AudioClip _actionUnavailableSound;

    private const string Log = "You must clear the previous stage.";

    protected override void PointerClick()
    {
        if(Managers.Data.user.GetStageState() != StageState.Locked)
        {
            _audioSource.clip = _actionAvailableSound;
            _button.interactable = false;
            
            Managers.Game.InitGame();
        }
        else
        {
            _audioSource.clip = _actionUnavailableSound;

            Managers.UI.Hide<ToastMessage_UI>();
            Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(Log);
        }
    }
}
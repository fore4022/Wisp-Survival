using UnityEngine;
public class Start_UI : Button_Default
{
    [SerializeField] private AudioClip actionAvailableSound;
    [SerializeField] private AudioClip actionUnavailableSound;

    private const string log = "You must clear the previous stage.";

    protected override void PointerClick()
    {
        if(Managers.Data.user.GetStageState() != StageState.Locked)
        {
            audioSource.clip = actionAvailableSound;
            button.interactable = false;
            
            Managers.Game.InitGame();
        }
        else
        {
            audioSource.clip = actionUnavailableSound;

            Managers.UI.Hide<ToastMessage_UI>();
            Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(log);
        }
    }
}
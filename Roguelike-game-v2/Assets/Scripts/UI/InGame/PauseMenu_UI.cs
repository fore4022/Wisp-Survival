using UnityEngine;
using UnityEngine.UI;
public class PauseMenu_UI : UserInterface
{
    [SerializeField] private Image[] icons;

    private bool attackSelectionActive = false;

    public override void SetUserInterface()
    {
        Managers.UI.Hide<PauseMenu_UI>();
    }
    protected override void Enable()
    {
        attackSelectionActive = Managers.UI.Get<SkillSelection_UI>().gameObject.activeSelf;

        if(attackSelectionActive)
        {
            Managers.UI.Get<SkillSelection_UI>().SkillOptionToggle(false);
        }
    }
    private void OnDisable()
    {
        if(attackSelectionActive)
        {
            Managers.UI.Get<SkillSelection_UI>().SkillOptionToggle(true);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void ShowIcons()
    {
        foreach(Image icon in icons)
        {
            icon.gameObject.SetActive(true);
        }
    }
    public void HideIcons()
    {
        foreach(Image icon in icons)
        {
            icon.gameObject.SetActive(false);
        }
    }
}
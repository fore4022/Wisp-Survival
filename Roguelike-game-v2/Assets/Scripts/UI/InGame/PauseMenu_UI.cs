using UnityEngine;
using UnityEngine.UI;
public class PauseMenu_UI : UserInterface
{
    [SerializeField] private Image[] _icons;

    private bool _attackSelectionActive = false;

    public override void SetUserInterface()
    {
        Managers.UI.Hide<PauseMenu_UI>();
    }
    protected override void Enable()
    {
        _attackSelectionActive = Managers.UI.Get<SkillSelection_UI>().gameObject.activeSelf;

        if(_attackSelectionActive)
        {
            Managers.UI.Get<SkillSelection_UI>().SkillOptionToggle(false);
        }
    }
    private void OnDisable()
    {
        if(_attackSelectionActive)
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
        foreach(Image icon in _icons)
        {
            icon.gameObject.SetActive(true);
        }
    }
    public void HideIcons()
    {
        foreach(Image icon in _icons)
        {
            icon.gameObject.SetActive(false);
        }
    }
}
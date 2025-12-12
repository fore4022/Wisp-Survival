using UnityEngine;
using UnityEngine.UI;
public class StageInformation_Button : Button_Default
{
    [SerializeField] private Color _visible;
    [SerializeField] private Color _invisible;

    private Image _image;

    private bool _isVisible = false;

    protected override void Init()
    {
        _image = GetComponent<Image>();

        base.Init();
    }
    protected override void PointerClick()
    {
        _isVisible = !_isVisible;

        InformationUpdate();
    }
    public void InformationUpdate()
    {
        if(_isVisible)
        {
            Show_StageInformation();
        }
        else
        {
            Hide_StageInformation();
        }
    }
    public void Show_StageInformation()
    {
        _image.color = _invisible;

        Managers.UI.Show<StageInformation_UI>();
    }
    public void Hide_StageInformation()
    {
        _image.color = _visible;

        Managers.UI.Hide<StageInformation_UI>();
    }
}
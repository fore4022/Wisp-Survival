using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillOption_UI : Button_B
{
    private List<TextMeshProUGUI> _textList = new();
    private SkillContext _info = null;
    private Image _image;
    private RectTransform _imageRect;

    protected override void PointerClick()
    {
        Managers.Game.inGameData_Manage.skill.SetValue(_info.data.type);
        Managers.UI.Get<SkillSelection_UI>().Selected();
    }
    protected override void Init()
    {
        base.Init();

        _image = transform.GetComponentInChild<Image>();
        _textList = transform.GetComponentsInChild<TextMeshProUGUI>();
        _imageRect = _image.gameObject.GetComponent<RectTransform>();

        Managers.Audio.Registration(_audioSource);
    }
    public void Reset()
    {
        _info = null;
    }
    public void InitOption(SkillContext info)
    {
        _info = info;

        transform.SetScale(_minScale);
        SetOption();
    }
    private void SetOption()
    {
        Vector2 size;

        _image.sprite = _info.data.icon;
        size = _image.sprite.bounds.size;

        if(size.x > size.y)
        {
            _imageRect.localScale = new Vector3(1, 1 * (size.y / size.x));
        }
        else if(size.y > size.x)
        {
            _imageRect.localScale = new Vector3(1 * (size.x / size.y), 1);
        }
        else
        {
            _imageRect.localScale = new Vector2(1, 1);
        }
        
        _textList[0].text = $"{_info.data.name}";

        if(_info.caster == null)
        {
            _textList[1].text = "New";
        }
        else
        {
            _textList[1].text = $"Lv. {_info.level + 1}";
        }

        _textList[2].text = $"{_info.data.explanation}";
    }
}
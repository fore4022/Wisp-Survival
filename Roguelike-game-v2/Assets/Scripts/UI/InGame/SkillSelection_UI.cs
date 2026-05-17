using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class SkillSelection_UI : UserInterface
{
    private List<SkillOption_UI> _skillOptionList = new();
    private Image _background;
    private GameObject _attackOption = null;

    private const string Path = "SkillOption";
    private const float Duration = 0.75f;
    private const float BasicAlpha = 180;
    private const int TargetAlpha = 0;

    private bool _isSelect = true;

    public int RequireOptionCount { get { return Managers.Game.inGameData_Manage.OptionCount - _skillOptionList.Count; } }
    public bool IsSelect { get { return _isSelect; } set { _isSelect = value; } }
    public override async void SetUserInterface()
    {
        _background = GetComponent<Image>();
        _attackOption = await AddressableHelper.LoadingToPath<GameObject>(Path);

        Managers.Game.inGameData_Manage.player.levelUpdate += () => Managers.UI.Show<LevelUp_UI>();

        StartCoroutine(Init());
    }
    public void SkillOptionToggle(bool active)
    {
        foreach(SkillOption_UI attackOption in _skillOptionList)
        {
            attackOption.gameObject.SetActive(active);
        }

        _background.enabled = active;
    }
    protected override void Enable()
    {
        Managers.UI.Show<SkillPoints_UI>();

        Set();
    }
    public void Set()
    {
        Managers.UI.Get<SkillPoints_UI>().SkillPointsUpdate();

        if(_skillOptionList.Count == 0)
        {
            return;
        }

        IsSelect = false;

        StartCoroutine(Setting());
    }
    public void Selected()
    {
        foreach(SkillOption_UI attackOption in _skillOptionList)
        {
            attackOption.gameObject.SetActive(false);
        }

        Managers.Game.inGameData_Manage.player.LevelUpCount--;
        _isSelect = true;

        StartCoroutine(SkillListUpdate());
    }
    private void OnDisable()
    {
        foreach(SkillOption_UI attackOption in _skillOptionList)
        {
            if(attackOption.enabled == true)
            {
                break;
            }

            attackOption.Reset();
            attackOption.gameObject.SetActive(false);
        }

        _background.enabled = false;

        InputManage.EnableInputAction<TouchControls>();
    }
    private void CreateOptionUI()
    {
        Transform trans = transform.GetChild(0);
        GameObject go;

        int count = RequireOptionCount;

        for (int i = 0; i < count; i++)
        {
            go = Instantiate(_attackOption, trans);

            _skillOptionList.Add(go.transform.GetComponentInChild<SkillOption_UI>());

            go.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private IEnumerator Init()
    {
        _background.enabled = false;

        yield return new WaitUntil(() => Managers.Game.inGameData_Manage != null);
        
        yield return new WaitUntil(() => _attackOption != null);

        CreateOptionUI();

        Managers.Game.inGameData_Manage.skillOptionCountUpdate += CreateOptionUI;

        gameObject.SetActive(false);
    }
    private IEnumerator Setting()
    {
        List<SkillContext> infoList = Managers.Game.inGameData_Manage.skill.GetSkill_Information();

        int[] indexArray = Default_Calculate.GetRandomValues(infoList.Count, Mathf.Min(Managers.Game.inGameData_Manage.OptionCount, infoList.Count));

        UIElementUtility.SetImageAlpha(_background, BasicAlpha);

        yield return new WaitForEndOfFrame();

        for(int i = 0; i < indexArray.Count(); i++)
        {
            _skillOptionList[i].gameObject.SetActive(true);
            _skillOptionList[i].InitOption(infoList[indexArray[i]]);
        }

        _background.enabled = true;
    }
    private IEnumerator PadeOut()
    {
        Managers.UI.Hide<SkillPoints_UI>();
        UIElementUtility.SetImageAlpha(_background, TargetAlpha, Duration);

        yield return new WaitForSecondsRealtime(Duration);

        Time.timeScale = 1f;
        Managers.Game.Playing = true;

        InputManage.EnableInputAction<TouchControls>();
        Managers.UI.Hide<SkillSelection_UI>();
    }
    private IEnumerator SkillListUpdate()
    {
        while(Managers.Game.inGameData_Manage.player.LevelUpCount > 0)
        {
            Set();

            yield return new WaitUntil(() => IsSelect);
        }

        StartCoroutine(PadeOut());
    }
}
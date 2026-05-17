using System;
using UnityEngine;
/// <summary>
/// skill option 개수 업데이트 action와 Player, Skill 정보 및 초기화
/// </summary>
public class InGameDataManage
{
    public Action skillOptionCountUpdate = null;
    public PlayerData player = new();
    public SkillDatas skill = new();

    private const int MaxOptionCount = 5;

    private int _optionCount = 3;
    
    public int OptionCount
    {
        get { return Mathf.Min(_optionCount, MaxOptionCount); }
        set
        {
            _optionCount = value;

            if(skillOptionCountUpdate != null)
            {
                skillOptionCountUpdate.Invoke();
            }
        }
    }
}
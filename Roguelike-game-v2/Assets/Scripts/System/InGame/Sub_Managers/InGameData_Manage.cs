using System;
using UnityEngine;
/// <summary>
/// skill option 개수 업데이트 action와 Player, Skill 정보 및 초기화
/// </summary>
public class InGameData_Manage
{
    public Action skillOptionCount_Update = null;
    public PlayerData player = new();
    public SkillDatas skill = new();

    private const int maxOptionCount = 5;

    private int optionCount = 3;
    
    public int OptionCount
    {
        get { return Mathf.Min(optionCount, maxOptionCount); }
        set
        {
            optionCount = value;

            if(skillOptionCount_Update != null)
            {
                skillOptionCount_Update.Invoke();
            }
        }
    }
}
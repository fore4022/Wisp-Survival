using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 플레이어의 레벨, 경험치, 정보를 관리하는 역할
/// </summary>

public class PlayerData
{
    public Action experienceUpdate = null;
    public Action levelUpdate = null;

    private Player_Information _info = null;

    private const float BaseExperience = 5;

    private Coroutine _levelCalculation = null;
    private int _increaseValue;
    private int _maxLevel;
    private int _levelUpCount = 1;

    public Player_Information Info { set { _info = value; } }

    public int IncreaseValue { get { return _increaseValue; } }

    public int MaxLevel { get { return _maxLevel; } set { _maxLevel = value * Skill_SO.MaxLevel; } }

    public int LevelUpCount { get { return _levelUpCount; } set { _levelUpCount = value; } }

    public int Level
    {
        get { return _info.level; }
        set
        {
            _info.level = value;
            _increaseValue = value;
            
            levelUpdate?.Invoke();
        }
    }

    public float Experience
    {
        get { return _info.experience; }
        set
        {
            _info.experience = value;

            if(Experience >= _info.experienceForLevelUp)
            {
                while(Experience >= _info.experienceForLevelUp)
                {
                    Level++;
                    _info.experience -= _info.experienceForLevelUp;
                    
                    if(Level <= _maxLevel)
                    {
                        _levelUpCount++;
                    }
                 
                    SetRequiredExperience();
                }

                if(_levelCalculation != null)
                {
                    CoroutineHelper.Stop(_levelCalculation, CoroutineType.Manage);
                }

                _levelCalculation = CoroutineHelper.Start(WaitLevelCalculation(), CoroutineType.Manage);
            }

            experienceUpdate?.Invoke();
        }
    }

    public float ExperienceForLevelUp { get { return _info.experienceForLevelUp; } }

    private void SetRequiredExperience()
    {
        _info.experienceForLevelUp += ExperienceForLevelUp * MathF.Max(0.75f - 0.195f * (Level - 1), 0.115f);
    }

    public void SetLevel()
    {
        _info.experienceForLevelUp = BaseExperience;
        _info.experience = 0;
        Level = 1;

        Managers.UI.Get<LevelText_UI>().LevelUpdate();
    }

    private IEnumerator WaitLevelCalculation()
    {
        for(int i = 0; i < 2; i++)
        {
            yield return null;
        }

        levelUpdate.Invoke();

        _levelCalculation = null;
    }
}
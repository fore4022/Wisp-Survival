using UnityEngine;

/// <summary>
/// 플레이어 스탯 비율과 레벨
/// </summary>

[System.Serializable]
public class PlayerStat
{
    [SerializeField] private int _moveSpeed = 0;
    [SerializeField] private int _increaseHealth = 0;
    [SerializeField] private int _increaseDamage = 0;
    [SerializeField] private int _healthRegenPerSec = 0;

    public DefaultStat defaultStat;

    private const string SceneName = "Main";
    private const float CoefMoveSpeed = 0.0525f;
    private const float CoefIncreaseHealth = 20f;
    private const float CoefIncreaseDamage = 4.85f;
    private const float CoefHealthRegenPerSec = 0.05f;

    public PlayerStat()
    {
        defaultStat = new(50, 10, 2, 0);
    }

    public float MoveSpeed
    {
        get
        {
            if(IsSceneMain())
            {
                return _moveSpeed;
            }
            else
            {
                return _moveSpeed * CoefMoveSpeed;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                _moveSpeed = (int)value;
            }
        }
    }

    public float IncreaseHealth
    {
        get
        {
            if(IsSceneMain())
            {
                return _increaseHealth;
            }
            else
            {
                return _increaseHealth * CoefIncreaseHealth;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                _increaseHealth = (int)value;
            }
        }
    }

    public float IncreaseDamage
    {
        get
        {
            if(IsSceneMain())
            {
                return _increaseDamage;
            }
            else
            {
                return _increaseDamage * CoefIncreaseDamage;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                _increaseDamage = (int)value;
            }
        }
    }

    public float HealthRegenPerSec
    {
        get
        {
            if(IsSceneMain())
            {
                return _healthRegenPerSec;
            }
            else
            {
                return _healthRegenPerSec * CoefHealthRegenPerSec;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                _healthRegenPerSec = (int)value;
            }
        }
    }

    public bool IsSceneMain()
    {
        return Managers.Scene.CurrentSceneName == SceneName;
    }
}
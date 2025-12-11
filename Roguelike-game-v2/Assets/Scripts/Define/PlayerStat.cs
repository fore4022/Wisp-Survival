using UnityEngine;
/// <summary>
/// 플레이어 스탯 비율과 레벨
/// </summary>
[System.Serializable]
public class PlayerStat
{
    [SerializeField] private int moveSpeed = 0;
    [SerializeField] private int increaseHealth = 0;
    [SerializeField] private int increaseDamage = 0;
    [SerializeField] private int healthRegenPerSec = 0;

    public DefaultStat defaultStat;

    private const string sceneName = "Main";
    private const float coef_MoveSpeed = 0.0525f;
    private const float coef_IncreaseHealth = 20f;
    private const float coef_IncreaseDamage = 4.85f;
    private const float coef_HealthRegenPerSec = 0.05f;

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
                return moveSpeed;
            }
            else
            {
                return moveSpeed * coef_MoveSpeed;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                moveSpeed = (int)value;
            }
        }
    }
    public float IncreaseHealth
    {
        get
        {
            if(IsSceneMain())
            {
                return increaseHealth;
            }
            else
            {
                return increaseHealth * coef_IncreaseHealth;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                increaseHealth = (int)value;
            }
        }
    }
    public float IncreaseDamage
    {
        get
        {
            if(IsSceneMain())
            {
                return increaseDamage;
            }
            else
            {
                return increaseDamage * coef_IncreaseDamage;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                increaseDamage = (int)value;
            }
        }
    }
    public float HealthRegenPerSec
    {
        get
        {
            if(IsSceneMain())
            {
                return healthRegenPerSec;
            }
            else
            {
                return healthRegenPerSec * coef_HealthRegenPerSec;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                healthRegenPerSec = (int)value;
            }
        }
    }
    public bool IsSceneMain()
    {
        return Managers.Scene.CurrentSceneName == sceneName;
    }
}
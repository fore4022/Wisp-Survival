using UnityEngine;
/// <summary>
/// 플레이어 스탯 관리
/// </summary>
public class PlayerStat_Manage : MonoBehaviour
{
    public const int maxLevel = 5;

    private float moveSpeed = 0;
    private float increaseHealth = 0;
    private float increaseDamage = 0;
    private float healthRegenPerSec = 0;

    public void Set(PlayerStat stat)
    {
        moveSpeed = stat.MoveSpeed;
        increaseHealth = stat.IncreaseHealth;
        increaseDamage = stat.IncreaseDamage;
        healthRegenPerSec = stat.HealthRegenPerSec;
    }
    public void Save()
    {
        Managers.Data.user.Stat.MoveSpeed = moveSpeed;
        Managers.Data.user.Stat.IncreaseHealth = increaseHealth;
        Managers.Data.user.Stat.IncreaseDamage = increaseDamage;
        Managers.Data.user.Stat.HealthRegenPerSec = healthRegenPerSec;
    }
}
using UnityEngine;
/// <summary>
/// 플레이어 스탯 관리
/// </summary>
public class PlayerStatManage : MonoBehaviour
{
    public const int MaxLevel = 5;

    private float _moveSpeed = 0;
    private float _increaseHealth = 0;
    private float _increaseDamage = 0;
    private float _healthRegenPerSec = 0;

    public void Set(PlayerStat stat)
    {
        _moveSpeed = stat.MoveSpeed;
        _increaseHealth = stat.IncreaseHealth;
        _increaseDamage = stat.IncreaseDamage;
        _healthRegenPerSec = stat.HealthRegenPerSec;
    }
    public void Save()
    {
        Managers.Data.user.Stat.MoveSpeed = _moveSpeed;
        Managers.Data.user.Stat.IncreaseHealth = _increaseHealth;
        Managers.Data.user.Stat.IncreaseDamage = _increaseDamage;
        Managers.Data.user.Stat.HealthRegenPerSec = _healthRegenPerSec;
    }
}
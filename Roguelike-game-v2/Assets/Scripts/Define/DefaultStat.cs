using System;

/// <summary>
/// 플레이어와 몬스터가 가지는 기본 스탯
/// </summary>

[Serializable]
public class DefaultStat
{
    [NonSerialized]
    public float maxHealth;

    public float health;
    public float damage;
    public float moveSpeed;
    public float healthRegenPerSec;

    public DefaultStat(float health, float damage, float moveSpeed, float healthRegenPerSec)
    {
        maxHealth = this.health = health;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.healthRegenPerSec = healthRegenPerSec;
    }

    public DefaultStat(DefaultStat stat, bool isPlayer = false)
    {
        if(isPlayer)
        {
            maxHealth = health = stat.health + Managers.Data.user.Stat.IncreaseHealth;
            damage = stat.damage + Managers.Data.user.Stat.IncreaseDamage;
            moveSpeed = stat.moveSpeed + Managers.Data.user.Stat.MoveSpeed;
            healthRegenPerSec = stat.healthRegenPerSec + Managers.Data.user.Stat.HealthRegenPerSec;
        }
        else
        {
            maxHealth = health = stat.health;
            damage = stat.damage;
            moveSpeed = stat.moveSpeed;
            healthRegenPerSec = stat.healthRegenPerSec;
        }
    }
}
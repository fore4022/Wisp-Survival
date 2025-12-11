using UnityEngine;
/// <summary>
/// 일정한 확률로 사망시, 현재 위치에 공격 시전
/// </summary>
public class Monster_G : BasicMonster_WithObject
{
    [SerializeField][Range(0, 100)] private float skillCastChance;

    protected string skillKey;

    protected override void Init()
    {
        skillKey = monsterSO.ExtraObjects[0].name;

        base.Init();
    }
    protected override void Die()
    {
        if(skillCastChance == 100)
        {
            SkillCast();
        }
        else
        {
            if(Random.Range(0, 100) <= skillCastChance)
            {
                SkillCast();
            }
        }

        base.Die();
    }
    protected virtual void SkillCast()
    {
        PoolingObject go = Managers.Game.objectPool.GetObject(skillKey);

        go.Transform.position = transform.position;

        go.SetActive(true);
    }
}
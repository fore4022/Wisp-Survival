using UnityEngine;

/// <summary>
/// 일정한 확률로 사망시, 현재 위치에 공격 시전
/// </summary>
/// <remarks>
/// 사용 객체 : EarthB, Potionl, ScorpionC
/// </remarks>

public class MonsterG : BasicMonster_WithObject
{
    [SerializeField][Range(0, 100)] private float _skillCastChance;

    protected string _skillKey;

    protected override void Init()
    {
        _skillKey = monsterSO.ExtraObjects[0].name;

        base.Init();
    }

    protected override void Die()
    {
        if(_skillCastChance == 100)
        {
            SkillCast();
        }
        else
        {
            if(Random.Range(0, 100) <= _skillCastChance)
            {
                SkillCast();
            }
        }

        base.Die();
    }

    protected virtual void SkillCast()
    {
        PoolingObject go = Managers.Game.objectPool.GetObject(_skillKey);

        go.Transform.position = transform.position;

        go.SetActive(true);
    }
}
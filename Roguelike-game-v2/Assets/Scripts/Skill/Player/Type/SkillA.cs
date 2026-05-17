using UnityEngine;
/// <summary>
/// 가장 가까운 적을 대상으로 범위 공격
/// </summary>
/// <remarks>
/// 사용 객체 : 
/// </remarks>
public class SkillA : PlayerSkill, IPlayerSkill
{
    public bool Finished { get { return true; } }
    public void Set()
    {
        transform.position = MonsterDetection.GetNearestMonsterPosition();
        transform.rotation = Default_Calculate.GetRandomQuaternion();
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}
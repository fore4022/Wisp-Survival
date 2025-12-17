using UnityEngine;
/// <summary>
/// 가장 큰 적 무리를 대상으로 범위 공격
/// </summary>
/// <remarks>
/// 사용 객체 : Void_Burst
/// </remarks>
public class Skill_B : PlayerSkill, IPlayerSkill
{
    public bool Finished { get { return true; } }
    public void Set()
    {
        transform.position = MonsterDetection.GetLargestMonsterGroup() + _so.AdjustmentPosition;
    }
    public void SetCollider()
    {
        _defaultCollider.enabled = true;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}
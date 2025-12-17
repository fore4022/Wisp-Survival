using UnityEngine;
/// <summary>
/// 화면에 보이는 무작위 적을 범위 공격
/// </summary>
/// <remarks>
/// 사용 객체 : Phantom_Blade, Thunderstroke
/// </remarks>
public class Skill_C : PlayerSkill, IPlayerSkill
{
    public bool Finished { get { return true; } }
    public void Set()
    {
        transform.position = MonsterDetection.GetRandomMonsterPosition();
    }
    public void SetCollider()
    {
        _playColliderOnEnable = !_playColliderOnEnable;
        _defaultCollider.enabled = _playColliderOnEnable;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}
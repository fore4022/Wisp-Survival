using UnityEngine;
/// <summary>
/// 가장 가까운 적 방향으로 범위 피해
/// </summary>
/// <remarks>
/// 사용 객체 : Sonic_Boom
/// </remarks>
public class Skill_G : PlayerSkill, IPlayerSkill
{
    [SerializeField] private float _skillRange;

    public bool Finished { get { return true; } }
    public void Set()
    {
        Vector3 direction = Default_Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition());

        transform.rotation = Default_Calculate.GetQuaternion(direction);
        transform.position = Managers.Game.player.transform.position + direction * _skillRange;
    }
    public void SetCollider()
    {
        _defaultCollider.enabled = !_defaultCollider.enabled;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}
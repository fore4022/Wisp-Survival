using System.Collections;
using UnityEngine;
/// <summary>
/// 가장 가까운 적에게 관통 투사체 공격
/// </summary>
/// <remarks>
/// 사용 객체 : Purifying_Cross
/// </remarks>
public class ProjectileA : PlayerSkillProjectile, IProjectile
{
    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Default_Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition());
        transform.rotation = Default_Calculate.GetQuaternion(direction - _so.AdjustmentRotation);
        moving = StartCoroutine(Moving());
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    public IEnumerator Moving()
    {
        while (true)
        {
            transform.position += direction * _so.Projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
}
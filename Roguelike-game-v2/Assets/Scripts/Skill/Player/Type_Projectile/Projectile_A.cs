using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 원거리 공격 / 연속 타격 원거리 공격
/// </para>
/// 가장 가까운 적을 공격
/// </summary>
public class Projectile_A : PlayerSkill_Projectile, IProjectile
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
using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 원거리 범위 공격
/// </para>
/// 가까운 적 또는 무작위 방향으로 시전
/// 스킬이 유지되는 동안 지정된 크기까지 커질 수 있으며, 그에 따라 피격 범위도 변동
/// </summary>
public class Projectile_D : PlayerSkill_Projectile, IProjectile
{
    [SerializeField][Range(0, 100)] private float probability;
    [SerializeField][Range(0.01f, 10)] private float targetScale;
    [SerializeField][Min(0.01f)] private float duration;

    private bool isInit = false;

    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Default_Calculate.GetRandomDirection();

        transform.SetScale(5.75f, duration, EaseType.OutCubic);
        
        if(Random.Range(0, 100) <= probability)
        {
            direction = Default_Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition());
        }
        else
        {
            direction = Default_Calculate.GetRandomDirection();
        }

        moving = StartCoroutine(Moving());
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    private void OnDisable()
    {
        if(isInit)
        {
            defaultCollider.enabled = false;

            transform.Kill().SetScale(1);
        }
        else
        {
            isInit = true;
        }
    }
    public IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * (Managers.Game.player.Stat.moveSpeed + so.Projectile_Info.speed) * Time.deltaTime;

            yield return null;
        }
    }
}
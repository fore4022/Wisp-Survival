using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격형 원거리 공격
/// </para>
/// 무작위 방향과 힘으로 날아가며, 지속 시간 동안 유지
/// </summary>
public class Projectile_C : PlayerSkill_Projectile, IProjectile
{
    [SerializeField] private float range;
    [SerializeField] private float min_Index;
    [SerializeField] private float max_Index;

    private float multiplier;
    private int sign;

    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Default_Calculate.GetDirection(Managers.Game.player.transform.position + (Vector3)Default_Calculate.GetRandomVector());
        multiplier = Random.Range(min_Index, max_Index + 1) * range + range;
        sign = Random.Range(0, 2);
        moving = StartCoroutine(Moving());

        if(sign == 0)
        {
            sign = -1;
        }

        StartCoroutine(AnimationManaging());
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
        while(true)
        {
            transform.position += direction * so.Projectile_Info.speed * multiplier * Time.deltaTime;
            multiplier -= Time.deltaTime;

            transform.Rotate(sign * Vector3.back * Time.timeScale);

            if(multiplier <= 0)
            {
                moving = null;

                yield break;
            }

            yield return null;
        }
    }
    private IEnumerator AnimationManaging()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        animator.Play("default");

        yield return new WaitUntil(() => multiplier <= 0.1f);
        
        animator.Play(so.Projectile_Info.animationName);
    }
}
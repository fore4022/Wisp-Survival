using System.Collections;
using UnityEngine;
/// <summary>
/// 스킬에 닿은 플레이어에게 피해를 주는 스킬
/// 고정된 위치에서 시전
/// </summary>
/// <remarks>
/// 사용 객체 : D1, E1, G1, H1
/// </remarks>
public class MonsterSkill_D : MonsterSkill_Damage
{
    protected override void Enable()
    {
        SetActive(true);
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    private IEnumerator Casting()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= GameUtil.AnimationEndTime);

        gameObject.SetActive(false);
    }
}
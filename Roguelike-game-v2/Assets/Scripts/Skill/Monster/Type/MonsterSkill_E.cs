using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSkill_E : MonsterSkill_Damage
{
    [SerializeField] private List<Color> colors;
    [SerializeField] private Vector3 skillOffset;

    private const float triggerTime = 0.975f;

    protected override void Enable()
    {
        render.color = colors[Random.Range(0, colors.Count)];

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
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= triggerTime);

        gameObject.SetActive(false);
    }
}
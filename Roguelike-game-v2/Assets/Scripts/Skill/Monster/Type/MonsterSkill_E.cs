using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSkill_E : MonsterSkill_Damage
{
    [SerializeField] private List<Color> _colors;
    [SerializeField] private Vector3 _skillOffset;

    protected override void Enable()
    {
        _render.color = _colors[Random.Range(0, _colors.Count)];

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
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MonsterSkill_A : MonsterSkill_Damage
{
    [SerializeField][Min(0.1f)] private float _speed = 1;

    private Vector3 _direction;

    protected override void Enable()
    {
        _direction = Default_Calculate.GetDirection(Managers.Game.player.transform.position, transform.position);
        transform.rotation = Default_Calculate.GetQuaternion(_direction);

        SetActive(true);
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        gameObject.SetActive(false);
    }
    protected override void SetActive(bool isActive)
    {
        _col.enabled = isActive;

        base.SetActive(isActive);
    }
    private IEnumerator Casting()
    {
        while(true)
        {
            transform.position += _direction * _speed * Time.deltaTime;

            yield return null;
        }
    }
}
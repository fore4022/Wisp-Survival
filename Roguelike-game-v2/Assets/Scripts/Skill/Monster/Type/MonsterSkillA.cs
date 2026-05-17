using System.Collections;
using UnityEngine;
/// <summary>
/// 플레이어를 향해서 날아가는 투사체 스킬
/// </summary>
/// <remarks>
/// 사용 객체 : A1, A2, A3, A4, A5, A6, A7
/// </remarks>
[RequireComponent(typeof(BoxCollider2D))]
public class MonsterSkillA : MonsterSkillDamage
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
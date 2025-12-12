using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 폭발형 원거리 공격
/// </para>
/// 가장 가까운 적을 향해서 날아가며, 충돌 시 범위 내 적을 공격
/// </summary>
public class Projectile_B : PlayerSkill_Projectile, IProjectile
{
    [SerializeField] private Collider2D _effectCollider;

    private bool _isExplosion = false;

    public bool Finished { get { return _isExplosion && _animator.GetCurrentAnimatorStateInfo(0).IsName(_so.Projectile_Info.animationName); } }
    public void Set()
    {
        _animator.Play("default");

        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Default_Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition());
        transform.rotation = Default_Calculate.GetQuaternion(direction);
        moving = StartCoroutine(Moving());
    }
    public void SetCollider()
    {
        if(_isExplosion)
        {
            _effectCollider.enabled = false;
            _defaultCollider.enabled = false;
        }
        else
        {
            _effectCollider.enabled = true;
            _defaultCollider.enabled = false;
        }
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        if(!_isExplosion)
        {
            _animator.Play(_so.Projectile_Info.animationName);
            StopCoroutine(moving);

            moving = null;
            _isExplosion = true;
        }
    }
    private void OnDisable()
    {
        _effectCollider.enabled = false;
        _isExplosion = false;
    }
    public IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * _so.Projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
}
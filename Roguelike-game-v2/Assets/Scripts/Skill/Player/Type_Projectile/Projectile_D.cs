using DG.Tweening;
using System.Collections;
using UnityEngine;
/// <summary>
/// 가까운 적 또는 무작위 방향으로 시전
/// 스킬이 유지되는 동안 지정된 크기까지 커질 수 있으며, 그에 따라 피격 범위도 변동
/// </summary>
/// <remarks>
/// 사용 객체 : Void_Vortex
/// </remarks>
public class Projectile_D : PlayerSkill_Projectile, IProjectile
{
    [SerializeField][Range(0, 100)] private float _probability;
    [SerializeField][Range(0.01f, 10)] private float _targetScale;
    [SerializeField][Min(0.01f)] private float _duration;

    private bool _isInit = false;

    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Default_Calculate.GetRandomDirection();

        transform.DOScale(_targetScale, _duration)
            .SetEase(Ease.OutCubic)
            .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        
        if(Random.Range(0, 100) <= _probability)
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
        if(_isInit)
        {
            transform.localScale = new(1, 1);
        }
        else
        {
            _isInit = true;
        }
    }
    public IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * (Managers.Game.player.Stat.moveSpeed + _so.Projectile_Info.speed) * Time.deltaTime;

            yield return null;
        }
    }
}
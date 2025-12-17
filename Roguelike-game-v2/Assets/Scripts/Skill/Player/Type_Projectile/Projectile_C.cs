using System.Collections;
using UnityEngine;
/// <summary>
/// 무작위 방향과 힘으로 날아가며, 지속 시간 동안 유지
/// </summary>
/// <remarks>
/// 사용 객체 : Pulse_Burst
/// </remarks>
public class Projectile_C : PlayerSkill_Projectile, IProjectile
{
    [SerializeField] private float _range;
    [SerializeField] private float _minIndex;
    [SerializeField] private float _maxIndex;

    private float _multiplier;
    private int _sign;

    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Default_Calculate.GetDirection(Managers.Game.player.transform.position + (Vector3)Default_Calculate.GetRandomVector());
        _multiplier = Random.Range(_minIndex, _maxIndex + 1) * _range + _range;
        _sign = Random.Range(0, 2);
        moving = StartCoroutine(Moving());

        if(_sign == 0)
        {
            _sign = -1;
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
            transform.position += direction * _so.Projectile_Info.speed * _multiplier * Time.deltaTime;
            _multiplier -= Time.deltaTime;

            transform.Rotate(_sign * Vector3.back * Time.timeScale);

            if(_multiplier <= 0)
            {
                moving = null;

                yield break;
            }

            yield return null;
        }
    }
    private IEnumerator AnimationManaging()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        _animator.Play("default");

        yield return new WaitUntil(() => _multiplier <= 0.1f);
        
        _animator.Play(_so.Projectile_Info.animationName);
    }
}
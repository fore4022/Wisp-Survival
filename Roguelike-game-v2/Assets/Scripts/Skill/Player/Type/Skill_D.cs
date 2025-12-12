using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 대기 애니메이션 재생 이후 적을 공격하며, 화면 상의 무작위 적을 공격
/// </summary>
public class Skill_D : PlayerSkill, IPlayerSkill
{
    [SerializeField] private string _animationName;

    public bool Finished { get { return _animator.GetCurrentAnimatorStateInfo(0).IsName(_animationName); } }
    public void Set()
    {
        transform.position = MonsterDetection.GetRandomMonsterPosition();

        _animator.Play("default", 0);
        StartCoroutine(Attacking());
    }
    public void SetCollider()
    {
        _defaultCollider.enabled = !_defaultCollider.enabled;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    private IEnumerator Attacking()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        _audioSource.Play();
        _animator.Play(_animationName, 0);
    }
}
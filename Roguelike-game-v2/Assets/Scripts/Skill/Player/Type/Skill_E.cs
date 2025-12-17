using UnityEngine;
/// <summary>
/// 가장 큰 적 무리를 공격하며
/// 시전될 때 주어진 애니메이션 중 무작위로 재생
/// </summary>
/// <remarks>
/// 사용 객체 : Divine_Radiance
/// </remarks>
public class Skill_E : PlayerSkill, IPlayerSkill
{
    [SerializeField] private int _minIndex;
    [SerializeField] private int _maxIndex;

    public bool Finished { get { return true; } }
    public void Set()
    {
        int rand = Random.Range(_minIndex, _maxIndex + 1);

        transform.position = MonsterDetection.GetLargestMonsterGroup();

        _animator.Play($"default_{rand}");
    }
    public void SetCollider()
    {
        _playColliderOnEnable = !_playColliderOnEnable;
        _defaultCollider.enabled = _playColliderOnEnable;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}
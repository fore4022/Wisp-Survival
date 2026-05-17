using System.Collections;
using UnityEngine;

/// <summary>
/// 빠른 속도로 플레이어를 향해서 이동, 유효 회전 제한
/// </summary>
/// <remarks>
/// 사용 객체 : 
/// </remarks>

public class MonsterK : BasicMonster
{
    [SerializeField] private float _dashSpeedMultiplier;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _targetDirectionMultiplier;

    private Coroutine _behavior = null;
    private WaitForSeconds _delay;

    protected override void Init()
    {
        base.Init();

        _delay = new(_dashDuration);
    }

    protected override void Enable()
    {
        base.Enable();
    
        _behavior = StartCoroutine(RepeatBehavior());
    }

    protected override void Die()
    {
        base.Die();

        _canSwitchDirection = true;

        StopCoroutine(_behavior);
    }

    private IEnumerator RepeatBehavior()
    {
        _canSwitchDirection = true;

        yield return new WaitForSeconds(Random.Range(_dashCooldown / 2, _dashCooldown));

        float totalTime = 0;

        _canSwitchDirection = false;

        while(totalTime != _dashDuration)
        {
            totalTime += Time.deltaTime;

            if(totalTime > _dashDuration)
            {
                totalTime = _dashDuration;
            }

            _speedMultiplier = Mathf.Lerp(SpeedMultiplierDefault, _dashSpeedMultiplier, totalTime / _dashDuration);
            _directionMultiplier = Mathf.Lerp(DirectionMultiplierDefault, _targetDirectionMultiplier, totalTime / _dashDuration);

            yield return null;
        }

        yield return _delay;

        StartCoroutine(RepeatBehavior());
    }
}
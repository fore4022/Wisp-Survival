using System.Collections;
using UnityEngine;

/// <summary>
/// 짧게 끊어서 플레이어를 향해 돌진
/// </summary>
/// <remarks>
/// 사용 객체 : SnakeF
/// </remarks>

public class Monster_C : BasicMonster
{
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldownMax;
    [SerializeField] private float _dashCastingTime;

    // 이동 속도 배율 재설정 및 행동 코루틴 호출
    protected override void Enable()
    {
        base.Enable();

        _speedMultiplier = SpeedMultiplierDefault;

        StartCoroutine(RepeatBehavior());
    }

    // 일정 시간 간격으로 속도 배율 조절을 통해서 돌진
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitForSeconds(Random.Range(0, _dashCooldownMax));

        float totalTime = 0;

        while(totalTime != _dashCastingTime)
        {
            totalTime += Time.deltaTime;

            if(totalTime > _dashCastingTime)
            {
                totalTime = _dashCastingTime;
            }

            _speedMultiplier = Mathf.Lerp(0, SpeedMultiplierDefault, totalTime / _dashCastingTime);

            yield return null;
        }

        totalTime = 0;

        yield return new WaitForSeconds(_dashCastingTime / 4);

        while(totalTime != _dashDuration)
        {
            totalTime += Time.deltaTime;

            if(totalTime > _dashDuration)
            {
                totalTime = _dashDuration;
            }

            _speedMultiplier = Mathf.Lerp(_dashSpeed, 0, totalTime / _dashDuration);

            yield return null;
        }

        totalTime = 0;

        while(totalTime != _dashCastingTime)
        {
            totalTime += Time.deltaTime;

            if(totalTime > _dashCastingTime)
            {
                totalTime = _dashCastingTime;
            }

            _speedMultiplier = Mathf.Lerp(SpeedMultiplierDefault, 0, totalTime / _dashCastingTime);

            yield return null;
        }

        StartCoroutine(RepeatBehavior());
    }
}
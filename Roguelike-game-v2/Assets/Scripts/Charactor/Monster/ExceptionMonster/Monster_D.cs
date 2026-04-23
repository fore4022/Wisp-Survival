using System.Collections;
using UnityEngine;

/// <summary>
/// 주기적으로 플레이어의 방향으로 돌진
/// </summary>
/// <remarks>
/// 사용 객체 : 
/// </remarks>

public class Monster_D : BasicMonster
{
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;

    private WaitForSeconds _cooldown;

    protected override void Init()
    {
        _cooldown = new(_dashCooldown);

        base.Init();
    }

    protected override void Enable()
    {
        base.Enable();

        StartCoroutine(RepeatBehavior());
    }

    private IEnumerator RepeatBehavior()
    {
        _canSwitchDirection = true;

        yield return _cooldown;

        if(_isVisible)
        {
            float totalTime = 0;

            _canSwitchDirection = false;

            while(totalTime != _dashDuration)
            {
                totalTime += Time.deltaTime;

                if(totalTime > _dashDuration)
                {
                    totalTime = _dashDuration;
                }

                _speedMultiplier = Mathf.Lerp(_dashSpeed, SpeedMultiplierDefault, totalTime / _dashDuration);

                yield return null;
            }

            _canSwitchDirection = true;
        }

        StartCoroutine(RepeatBehavior());
    }
}
using System.Collections;
using UnityEngine;
/// <summary>
/// 플레이어 방향으로 빠르게 돌진
/// </summary>
/// <remarks>
/// 사용 객체 : AirA
/// </remarks>
public class Monster_E : BasicMonster
{
    [SerializeField] private float _rushSpeed = 2.7f;
    [SerializeField] private float _rushCastingTime = 0.9f;

    protected override void Enable()
    {
        base.Enable();

        _speedMultiplier = SpeedMultiplierDefault;
        _canFlipX = true;
        _canSwitchDirection = true;

        StartCoroutine(RepeatBehavior());
    }
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitUntil(() => _isVisible);

        yield return new WaitUntil(() => (Managers.Game.player.transform.position - transform.position).magnitude <= CameraUtil.CameraWidth / 2);

        float totalTime = 0;

        _canSwitchDirection = false;

        while (totalTime != _rushCastingTime)
        {
            totalTime += Time.deltaTime;

            if(totalTime > _rushCastingTime)
            {
                totalTime = _rushCastingTime;
            }

            _speedMultiplier = Mathf.Lerp(0, SpeedMultiplierDefault, totalTime / _rushCastingTime);

            yield return null;
        }

        _canFlipX = false;
        _speedMultiplier = _rushSpeed;
    }
}
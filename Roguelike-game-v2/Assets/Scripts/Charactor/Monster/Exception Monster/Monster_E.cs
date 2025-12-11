using System.Collections;
using UnityEngine;
/// <summary>
/// 플레이어 방향으로 빠르게 돌진
/// </summary>
public class Monster_E : BasicMonster
{
    [SerializeField] private float rushSpeed = 3;
    [SerializeField] private float rushCastingTime = 0.75f;

    protected override void Enable()
    {
        base.Enable();

        speedMultiplier = speedMultiplierDefault;
        canSwitchDirection = true;

        StartCoroutine(RepeatBehavior());
    }
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitUntil(() => isVisible);

        yield return new WaitUntil(() => (Managers.Game.player.transform.position - transform.position).magnitude <= CameraUtil.CameraWidth / 2);

        float totalTime = 0;

        canSwitchDirection = false;

        while (totalTime != rushCastingTime)
        {
            totalTime += Time.deltaTime;

            if(totalTime > rushCastingTime)
            {
                totalTime = rushCastingTime;
            }

            speedMultiplier = Mathf.Lerp(0, speedMultiplierDefault, totalTime / rushCastingTime);

            yield return null;
        }

        speedMultiplier = rushSpeed;
    }
}
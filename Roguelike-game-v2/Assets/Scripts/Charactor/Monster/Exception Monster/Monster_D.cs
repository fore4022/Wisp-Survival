using System.Collections;
using UnityEngine;
/// <summary>
/// 주기적으로 플레이어의 방향으로 돌진
/// </summary>
public class Monster_D : BasicMonster
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private WaitForSeconds cooldown;

    protected override void Init()
    {
        cooldown = new(dashCooldown);

        base.Init();
    }
    protected override void Enable()
    {
        base.Enable();

        StartCoroutine(RepeatBehavior());
    }
    private IEnumerator RepeatBehavior()
    {
        canSwitchDirection = true;

        yield return cooldown;

        if(isVisible)
        {
            float totalTime = 0;

            canSwitchDirection = false;

            while(totalTime != dashDuration)
            {
                totalTime += Time.deltaTime;

                if(totalTime > dashDuration)
                {
                    totalTime = dashDuration;
                }

                speedMultiplier = Mathf.Lerp(dashSpeed, speedMultiplierDefault, Ease.InExpo(totalTime / dashDuration));

                yield return null;
            }

            canSwitchDirection = true;
        }

        StartCoroutine(RepeatBehavior());
    }
}
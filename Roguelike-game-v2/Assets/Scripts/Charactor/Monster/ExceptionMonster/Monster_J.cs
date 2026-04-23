using System.Collections;
using UnityEngine;

/// <summary>
/// 수명동안 플레이어를 향해 이동
/// </summary>
/// <remarks>
/// 사용 객체 : BugC
/// </remarks>

[RequireComponent(typeof(CircleCollider2D))]
public class Monster_J : BasicMonster
{
    [SerializeField] private float _lifeTime;

    private WaitForSeconds _delay;

    protected override void Init()
    {
        _delay = new(_lifeTime);

        base.Init();
    }

    protected override void Enable()
    {
        _speedMultiplier = SpeedMultiplierDefault;

        base.Enable();
        StartCoroutine(RepeatBehavior());
    }

    protected override void Die()
    {
        StartCoroutine(GradualSlowdown());
        StartCoroutine(Dieing());
    }

    private IEnumerator RepeatBehavior()
    {
        yield return _delay;

        Die();
    }

    private IEnumerator GradualSlowdown()
    {
        float totalTime = 0;

        while(totalTime != Death_AnimationDuration)
        {
            totalTime += Time.deltaTime;

            if(totalTime > Death_AnimationDuration)
            {
                totalTime = Death_AnimationDuration;
            }

            _speedMultiplier = Mathf.Lerp(0, SpeedMultiplierDefault, totalTime / Death_AnimationDuration);

            yield return null;
        }
    }
}
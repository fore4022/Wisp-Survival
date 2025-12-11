using System.Collections;
using UnityEngine;
/// <summary>
/// 쿨타임마다 현재 위치에서 스킬 시전
/// </summary>
public class Monster_N : BasicMonster_WithObject
{
    [SerializeField] private Vector3 skillRotation;
    [SerializeField] private float skillPositionX;
    [SerializeField] private float skillPositionY;
    [SerializeField] private float skillDuration;
    [SerializeField] private float skillRange;
    [SerializeField] private float skillCooldown;

    private const float triggerTime = 0.975f;

    private Coroutine behavior = null;
    private WaitForSeconds cooldown;
    private WaitForSeconds delay;
    private string skillKey;

    protected override void Init()
    {
        delay = new(skillDuration);
        cooldown = new(skillCooldown);
        skillKey = monsterSO.ExtraObjects[0].name;

        base.Init();
    }
    protected override void Enable()
    {
        speedMultiplier = 1;
        canSwitchDirection = true;
        behavior = StartCoroutine(RepeatBehavior());

        base.Enable();
    }
    protected override void Die()
    {
        base.Die();

        StopCoroutine(behavior);
    }
    private IEnumerator RepeatBehavior()
    {
        yield return cooldown;

        if(isVisible)
        {
            float sign;

            speedMultiplier = 0;
            canSwitchDirection = false;
            sign = render.flipX ? 1 : -1;

            transform.SetRotation(-skillRotation * sign, skillDuration, EaseType.InQuad)
                .SetRotation(skillRotation * sign, skillDuration * 2, EaseType.InQuad, TweenOperation.Append);

            yield return delay;

            PoolingObject go = Managers.Game.objectPool.GetObject(skillKey);

            go.SpriteRenderer.flipX = sign == 1 ? false : true;
            go.Transform.position = transform.position + new Vector3(skillPositionX * sign, skillPositionY);

            go.SetActive(true);

            yield return new WaitUntil(() => !go.activeSelf);

            speedMultiplier = 1;
            canSwitchDirection = true;
        }

        behavior = StartCoroutine(RepeatBehavior());
    }
}
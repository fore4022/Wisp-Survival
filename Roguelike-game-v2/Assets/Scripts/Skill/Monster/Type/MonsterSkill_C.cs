using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSkill_C : MonsterSkill
{
    [SerializeField][Min(1)] private float slowDown = 0;
    [SerializeField][Min(0.1f)] private float slowDuration;
    [SerializeField] private float offsetY;
    [SerializeField] private float targetScale;

    private const float triggerTime = 0.975f;

    private float defaultOffsetY;
    private float defaultRadius;

    protected new CircleCollider2D col { get { return base.col as CircleCollider2D; } }
    protected override void Init()
    {
        base.Init();

        defaultOffsetY = col.offset.y;
        defaultRadius = col.radius;
    }
    protected override void Enable()
    {
        SetActive(true);
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        Managers.Game.player.move.SetSlowDown(slowDown, slowDuration);
    }
    protected override void Disable()
    {
        col.offset = new(0, defaultOffsetY);
        col.radius = defaultRadius;

        base.Disable();
    }
    private float GetVale()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime / triggerTime;
    }
    private IEnumerator Casting()
    {
        Vector2 lerpedOffset = new();
        float lerpedScale;
        float value;

        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= triggerTime)
        {
            value = GetVale();
            lerpedOffset.y = Mathf.Lerp(offsetY, defaultOffsetY, value);
            lerpedScale = Mathf.Lerp(targetScale, defaultRadius, value);
            col.offset = lerpedOffset;
            col.radius = lerpedScale;

            yield return null;
        }

        SetActive(false);
    }
}
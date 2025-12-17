using System.Collections;
using UnityEngine;
/// <summary>
/// 플레이어에 닿을 경우 둔화하는 스킬
/// </summary>
/// <remarks>
/// 사용 객체 : C1
/// </remarks>
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSkill_C : MonsterSkill
{
    [SerializeField][Min(1)] private float _slowDown = 0;
    [SerializeField][Min(0.1f)] private float _slowDuration;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _targetScale;

    private float _defaultOffsetY;
    private float _defaultRadius;

    protected CircleCollider2D col { get { return _col as CircleCollider2D; } }
    protected override void Init()
    {
        base.Init();

        _defaultOffsetY = col.offset.y;
        _defaultRadius = col.radius;
    }
    protected override void Enable()
    {
        SetActive(true);
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        Managers.Game.player.move.SetSlowDown(_slowDown, _slowDuration);
    }
    protected override void Disable()
    {
        col.offset = new(0, _defaultOffsetY);
        col.radius = _defaultRadius;

        base.Disable();
    }
    private float GetVale()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime / GameUtil.AnimationEndTime;
    }
    private IEnumerator Casting()
    {
        Vector2 lerpedOffset = new();
        float lerpedScale;
        float value;

        while(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= GameUtil.AnimationEndTime)
        {
            value = GetVale();
            lerpedOffset.y = Mathf.Lerp(_offsetY, _defaultOffsetY, value);
            lerpedScale = Mathf.Lerp(_targetScale, _defaultRadius, value);
            col.offset = lerpedOffset;
            col.radius = lerpedScale;

            yield return null;
        }

        SetActive(false);
    }
}
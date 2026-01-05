using DG.Tweening;
using System.Collections;
using UnityEngine;
/// <summary>
/// 쿨타임마다 현재 위치에서 스킬 시전
/// </summary>
/// <remarks>
/// 사용 객체 : SwordA
/// </remarks>
public class Monster_N : BasicMonster_WithObject
{
    [SerializeField] private Vector3 _skillRotation;
    [SerializeField] private float _skillPositionX;
    [SerializeField] private float _skillPositionY;
    [SerializeField] private float _skillDuration;
    [SerializeField] private float _skillRange;
    [SerializeField] private float _skillCooldown;

    private Coroutine _behavior = null;
    private WaitForSeconds _cooldown;
    private WaitForSeconds _delay;
    private string _skillKey;

    protected override void Init()
    {
        _delay = new(_skillDuration);
        _cooldown = new(_skillCooldown);
        _skillKey = monsterSO.ExtraObjects[0].name;

        base.Init();
    }
    protected override void Enable()
    {
        _speedMultiplier = 1;
        _canSwitchDirection = true;
        _behavior = StartCoroutine(RepeatBehavior());

        base.Enable();
    }
    protected override void Die()
    {
        base.Die();

        StopCoroutine(_behavior);
    }
    private IEnumerator RepeatBehavior()
    {
        yield return _cooldown;

        if(_isVisible)
        {
            float sign;

            _speedMultiplier = 0;
            _canSwitchDirection = false;
            sign = _render.flipX ? 1 : -1;

            transform.DORotate(-_skillRotation * sign, _skillDuration, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InQuad)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable)
                .OnComplete(() =>
                {
                    transform.DORotate(_skillRotation * sign, _skillDuration * 2, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InQuad)
                    .SetLink(gameObject, LinkBehaviour.KillOnDisable);
                });

            yield return _delay;

            PoolingObject go = Managers.Game.objectPool.GetObject(_skillKey);

            go.SpriteRenderer.flipX = sign == 1 ? false : true;
            go.Transform.position = transform.position + new Vector3(_skillPositionX * sign, _skillPositionY);

            go.SetActive(true);

            yield return new WaitUntil(() => !go.ActiveSelf);

            _speedMultiplier = 1;
            _canSwitchDirection = true;
        }

        _behavior = StartCoroutine(RepeatBehavior());
    }
}
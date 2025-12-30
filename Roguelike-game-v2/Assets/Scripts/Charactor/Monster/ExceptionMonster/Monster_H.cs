using System.Collections;
using UnityEngine;
/// <summary>
/// 일정 확률로 플레이어에게 돌진하며, 돌진이 끝난 후, 현재 위치에서 스킬 시전
/// </summary>
/// <remarks>
/// 사용 객체 : ReaperA
/// </remarks>
public class Monster_H : BasicMonster_WithObject
{
    [SerializeField] private Vector3 _skillPosition;
    [SerializeField] private string _skillAnimationName;
    [SerializeField][Range(0, 100)] private float _skillCastChance;
    [SerializeField] private float _skillDuration;
    [SerializeField] private float _skillCooldown;
    [SerializeField] private float _skillDelay;

    private const string _defaultAnimationName = "Walk";

    private WaitForSeconds _cooldown;
    private WaitForSeconds _delay;
    private string _skillKey;
    private bool _isEnterPlayer = false;

    protected override void Init()
    {
        _cooldown = new(_skillCooldown);
        _delay = new(_skillDelay);
        _skillKey = monsterSO.ExtraObjects[0].name;

        base.Init();
    }
    protected override void Enable()
    {
        base.Enable();

        StartCoroutine(RepeatBehavior());
    }
    protected override void Attack()
    {
        base.Attack();

        _isEnterPlayer = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _isEnterPlayer = false;
    }
    private IEnumerator RepeatBehavior()
    {
        _animator.Play(_defaultAnimationName);

        _speedMultiplier = 1;
        _canSwitchDirection = true;

        yield return _cooldown;

        if(Random.Range(0, 100) <= _skillCastChance)
        {            
            _speedMultiplier = 0;
            _canSwitchDirection = false;

            yield return _delay;

            float totalTime = 0;

            _animator.speed = 0;
            _speedMultiplier = 3;

            while(totalTime != _skillDuration)
            {
                totalTime += Time.deltaTime;

                if(totalTime > _skillDuration)
                {
                    totalTime = _skillDuration;
                }

                if(_isEnterPlayer)
                {
                    break;
                }

                yield return null;
            }

            if(_isVisible)
            {
                PoolingObject go = Managers.Game.objectPool.GetObject(_skillKey);

                float sign = _render.flipX ? 1 : -1;

                _animator.speed = 1;
                _speedMultiplier = 0;
                go.Transform.position = transform.position + _skillPosition * sign;
                go.Transform.localScale = new(sign, 1, 1);

                go.SetActive(true);
                _animator.Play(_skillAnimationName);

                yield return new WaitUntil(() => !go.activeSelf);
            }
        }

        StartCoroutine(RepeatBehavior());
    }
}
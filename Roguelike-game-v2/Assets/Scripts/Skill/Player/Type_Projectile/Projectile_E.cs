using DG.Tweening;
using System.Collections;
using UnityEngine;
/// <summary>
/// 화면의 무작위 위치를 향해 관통하는 투사체로, 위치에 도달할 경우 폭발
/// </summary>
/// <remarks>
/// 사용 객체 : Energy_Spear
/// </remarks>
public class Projectile_E : PlayerSkill_Projectile, IPlayerSkill
{
    [SerializeField] private Collider2D _effectCollider;

    [SerializeField] private Vector2 _castRange;
    [SerializeField][Min(0.01f)] private float _castDelay;

    private const int InitialRotationAngleMax = 1080;
    private const int InitialRotationAngleMin = 720;
    private const int AnimationAngle = 30;

    private Vector3 _targetPosition;
    private Vector2 _castingPosition;
    private float _signAngle;
    private bool _isExplosion = false;

    public bool Finished { get { return _isExplosion && _animator.GetCurrentAnimatorStateInfo(0).IsName(_so.Projectile_Info.animationName); } }
    public void Set()
    {
        _animator.Play("default");

        _castingPosition = _so.AdjustmentPosition + new Vector2(Random.Range(-_castRange.x / 2, _castRange.x / 2), Random.Range(-_castRange.y / 2, _castRange.y / 2));
        transform.position = Managers.Game.player.transform.position;
        transform.rotation = Default_Calculate.GetQuaternion(Default_Calculate.GetRandomVector());

        StartCoroutine(Attacking());
    }
    public void SetCollider()
    {
        if(_isExplosion)
        {
            _effectCollider.enabled = false;
            _defaultCollider.enabled = false;
        }
        else
        {
            _effectCollider.enabled = true;
            _defaultCollider.enabled = false;
        }
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    private void OnDisable()
    {
        transform.rotation = Quaternion.identity;
        _effectCollider.enabled = false;
        _isExplosion = false;
    }
    private IEnumerator Attacking()
    {
        float totalTime = 0;
        
        transform.DORotate(new(0, 0, Random.Range(InitialRotationAngleMin, InitialRotationAngleMax)), _castDelay * 2, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic);

        while(totalTime != _castDelay)
        {
            totalTime += Time.deltaTime;

            if(totalTime > _castDelay)
            {
                totalTime = _castDelay;
            }

            transform.position = Managers.Game.player.transform.position + Vector3.Lerp(new(), _castingPosition, totalTime / _castDelay);

            yield return null;
        }

        _targetPosition = MonsterDetection.GetNearestMonsterPosition(transform);

        yield return new WaitForSeconds(_castDelay);

        direction = Default_Calculate.GetDirection(_targetPosition, (Vector2)Managers.Game.player.transform.position + _castingPosition);
        _signAngle = Random.Range(0, 2) == 1 ? -1 : 1;

        transform.DORotate(Default_Calculate.GetQuaternion(direction).eulerAngles + new Vector3(0, 0, (360 + AnimationAngle * _signAngle) - transform.rotation.eulerAngles.z % 360), _castDelay, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
            {
                transform.DORotate(new(0, 0, AnimationAngle * -_signAngle), _castDelay * 2, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCubic);
            });

        yield return new WaitForSeconds(_castDelay * 3);

        Vector3 remainingDistance = _targetPosition - (transform.position + direction * _so.Projectile_Info.speed * Time.deltaTime);
        Vector3 afterPosition = new();

        totalTime = 0;
        _animator.speed = 1;
        _defaultCollider.enabled = true;

        while(true)
        {
            totalTime += Time.deltaTime;
            afterPosition = transform.position + direction * _so.Projectile_Info.speed * Time.deltaTime;

            if(remainingDistance.sqrMagnitude >= (_targetPosition - afterPosition).sqrMagnitude)
            {
                remainingDistance = _targetPosition - afterPosition;
                transform.position = afterPosition;
            }
            else
            {
                transform.position = _targetPosition;

                break;
            }

            yield return null;
        }

        _animator.Play(_so.Projectile_Info.animationName);

        _isExplosion = true;
    }
}
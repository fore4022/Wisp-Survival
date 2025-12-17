using System.Collections;
using UnityEngine;
/// <summary>
/// 지속 시간 동안 스킬에서 가까운 적을 향해 이동
/// </summary>
/// <remarks>
/// 사용 객체 : Flame_Tempest
/// </remarks>
public class Skill_F : PlayerSkill, IPlayerSkill
{
    [SerializeField] private float _speed;

    private Coroutine _colorVairation = null;
    private Vector3 _direction;
    private float _currentSpeed;
    private float _totalTime = 0;
    private float _targetTime = 0;

    public bool Finished { get { return _so.Duration <= _totalTime; } }
    public void Set()
    {
        _currentSpeed = _speed;
        _totalTime = 0;
        _targetTime = Mathf.Lerp(_totalTime, _so.Duration, Random.Range(1, _so.Duration) / _so.Duration);
        transform.position = Managers.Game.player.gameObject.transform.position + (Vector3)Default_Calculate.GetRandomVector();
        _direction = Default_Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition(), transform.position);

        StartCoroutine(Attacking());
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    private IEnumerator Attacking()
    {
        while(_totalTime < _so.Duration)
        {
            if(_totalTime >= _targetTime)
            {
                if(_totalTime < _so.Duration - 1)
                {
                    _targetTime = Mathf.Lerp(_totalTime, _so.Duration, Random.Range(1, _so.Duration) / _so.Duration);
                    _direction = Default_Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition(), transform.position);
                }
            }

            transform.position += _direction * _currentSpeed * Time.deltaTime;
            _totalTime += Time.deltaTime;
            
            yield return null;

            if(_totalTime > _so.Duration - 1)
            {
                _currentSpeed -= Time.deltaTime;

                if(_colorVairation == null)
                {
                    _colorVairation = StartCoroutine(ColorUtil.ChangeAlpha(_render, 0, _render.color.a, 1));
                }
            }
            else
            {
                _currentSpeed = Default_Calculate.GetParabolicY(_so.Duration, _speed, _totalTime) + 1;
            }
        }

        StopCoroutine(_colorVairation);

        _colorVairation = null;
        _render.color = Color.white;
    }
}
using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 플레이어를 향해서 이동하는 기본 몬스터
/// </para>
/// IDamage, IDamageReceiver, IMoveable을 구현
/// </summary>
/// <remarks>
/// 사용 객체 : BatSmallA, ChestA, CloudD, FactoryB, GhastC, MaskA, MummyA, SkullFlamingB
/// </remarks>
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    protected const float SpeedMultiplierDefault = 1;
    protected const float DamageMultiplierDefault = 1;
    protected const float DirectionMultiplierDefault = 1;
    protected const float Death_AnimationDuration = 0.3f;

    protected Action _onDamaged = null;
    
    protected Color _defaultColor;
    protected Vector2 _direction = default;
    protected float _speedMultiplier = SpeedMultiplierDefault;
    protected float _damageMultiplier = DamageMultiplierDefault;
    protected float _directionMultiplier = DirectionMultiplierDefault;
    protected bool _canSwitchDirection = true;

    private IMoveable _moveable;

    private const float DamagedDuration = 0.15f;
    
    private Coroutine _moveCoroutine = null;
    private WaitForSeconds _damaged = new(DamagedDuration);

    public float SpeedAmount { get { return _stat.moveSpeed * _speedMultiplier * SlowDownAmount; } }
    public float SlowDownAmount { get { return _moveable.SlowDownAmount; } }
    public float DamageAmount { get { return _stat.damage * _damageMultiplier * Managers.Game.difficultyScaler.IncreaseStat * Time.deltaTime; } }
    // IMoveable 구현
    protected override void Awake()
    {
        _moveable = new DefaultMoveable();

        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        Enable();
    }
    // 컴포넌트 설정
    protected override void Set()
    {
        base.Set();

        _render.color = _defaultColor;
        _render.enabled = true;
        _rigid.simulated = true;
    }
    // 위치 조정 및 이동 코루틴 실행
    protected virtual void Enable()
    {
        SetPosition();
        Set();
        _animator.Play(0, 0);

        _moveCoroutine = StartCoroutine(Moving());
    }
    // 이동 속도 감소
    public void SetSlowDown(float slowDown, float duration)
    {
        _moveable.SetSlowDown(slowDown, duration);
    }
    // 이동 처리
    public virtual void OnMove()
    {
        _rigid.linearVelocity = _direction * SpeedAmount;
    }
    // 자기 자신의 위치를 기준으로 플레이어로 향하는 방향 구하기, 배율에 따른 유효 회전 제한
    protected virtual void SetDirection()
    {
        if(!Managers.Game.GameOver)
        {
            if(_canSwitchDirection)
            {
                if(_direction == default)
                {
                    _direction = Default_Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);
                }
                else
                {
                    if(_directionMultiplier == 0)
                    {
                        return;
                    }

                    _direction = Vector3.Slerp(_direction, Default_Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position), _directionMultiplier).normalized;
                }
            }
        }
    }
    // 피격 효과 재생
    protected virtual void Damaged()
    {
        StartCoroutine(TakingDamage());
    }
    // 충돌 비활성화, 이동 중지
    protected virtual void Die()
    {
        _rigid.simulated = false;

        StopCoroutine(_moveCoroutine);
    }
    // 충돌 : Collision
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Enter(collision);
    }
    // 충돌 : Trigger
    protected void OnCollisionStay2D(Collision2D collision)
    {
        Enter(collision);
    }
    // 이벤트 호출, 데미지 로그 출력, 사망 확인
    public void TakeDamage(IDamage damage)
    {
        _health -= damage.DamageAmount;

        Managers.Game.damageLog_Manage.Show(transform.position, damage.DamageAmount);
        _onDamaged.Invoke();

        if(_health <= 0)
        {
            Die();
            StartCoroutine(Dieing());
        }
    }
    // 이벤트 등록 및 초기화
    protected override void Init()
    {
        base.Init();

        _onDamaged += _audioSource.Play;
        _onDamaged += Damaged;

        _defaultColor = _render.color;
    }
    // 충돌 대상 확인, 플레이어일 경우 공격 수행
    private void Enter(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
    // 플레이어 공격
    protected virtual void Attack()
    {
        Managers.Game.player.TakeDamage(this);
    }
    // 이동 및 방향 전환 코루틴, 카메라 영역에 보이는 경우 FlipX 실행
    private IEnumerator Moving()
    {
        while(true)
        {
            if(_isVisible)
            {
                FlipX();
            }

            SetDirection();
            OnMove();

            yield return null;
        }
    }
    // 사망 효과, 경험치 지급, 오브젝트 풀 반환
    protected virtual IEnumerator Dieing()
    {
        _animator.speed = 0;

        StartCoroutine(ColorUtil.ChangeColor(_render, Color.black, _defaultColor, Death_AnimationDuration / 2));

        yield return new WaitForSeconds(Death_AnimationDuration / 2);

        Managers.Game.inGameData_Manage.player.Experience += _user_Experience;
        Managers.Game.UserExp += _inGameExperience;
        _speedMultiplier = SpeedMultiplierDefault;
        _damageMultiplier = DamageMultiplierDefault;
        _directionMultiplier = DirectionMultiplierDefault;

        StartCoroutine(ColorUtil.ChangeAlpha(_render, 0, _render.color.a, Death_AnimationDuration));

        yield return new WaitForSeconds(Death_AnimationDuration);

        _render.color = _defaultColor;

        Managers.Game.objectPool.DisableObject(gameObject, _monsterSO.name);
    }
    // 피격 효과
    private IEnumerator TakingDamage()
    {
        _render.material.SetFloat("_Float", 1);

        yield return _damaged;

        _render.material.SetFloat("_Float", 0);
    }
}
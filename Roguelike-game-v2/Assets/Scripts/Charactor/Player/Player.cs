using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 게임 플레이 준비 완료 후에 초기화 작업을 진행
/// 플레이어가 움직이는 기능은 PlayerMove.cs로 나누어 구현
/// </summary>
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move = null;
    public Action maxHealthUpdate = null;
    public Action healthUpdate = null;

    private const float Duration = 0.4f;

    private Player_Information _information = new();
    private DefaultStat _stat = null;
    private SpriteRenderer _render;
    private Animator _animator;

    private bool _death = false;

    public DefaultStat Stat { get { return _stat; } }
    public float MaxHealth
    {
        get { return _stat.maxHealth; }
        set
        {
            _stat.maxHealth = value;

            maxHealthUpdate?.Invoke();
        }
    }
    public float Health
    {
        get { return _stat.health; }
        set
        {
            _stat.health = value;

            healthUpdate?.Invoke();
        }
    }
    public bool Death { get { return _death; } }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
        move = new(_render, new DefaultMoveable());
    }
    private void Start()
    {
        StartCoroutine(Init());
    }
    private void Update()
    {
        Health = Mathf.Min(Health + (_stat.healthRegenPerSec + ((MaxHealth - 50) % 20 / 200)) * Time.deltaTime, MaxHealth);
        move.IsPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }
    public void TakeDamage(IDamage damage)
    {
        Health -= damage.DamageAmount;
        
        if(_information.stat.health <= 0 && !_death)
        {
            _death = true;

            Die();
            Managers.Game.Over();
        }
    }
    public void Reset()
    {
        transform.localScale = new Vector2(3, 3);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _death = false;

        LoadPlayerStat();
        maxHealthUpdate?.Invoke();
        healthUpdate?.Invoke();
        _animator.Play("idle");
    }
    public void AnimationPlay(string animationName)
    {
        _animator.Play(animationName);
    }
    private void LoadPlayerStat()
    {
        _stat = _information.stat = new(Managers.Data.user.Stat.defaultStat, true);
    }
    private void Die()
    {
        _render.sortingLayerID = SortingLayer.NameToID("AboveEffect");
        _render.flipX = false;

        _animator.Play("death");
        transform.SetRotation(new(0, 0, 0))
            .SetScale(10, Duration)
            .SetPosition(transform.position + new Vector3(0, 0.5f), Duration)
            .SetRotation(new(0, 0, 370), Duration);
    }
    private IEnumerator Init()
    {
        LoadPlayerStat();

        Managers.Game.inGameData_Manage.player.Info = _information;

        yield return new WaitUntil(() => _information.stat != null);

        Managers.Game.player = this;

        move.Init();
    }
}
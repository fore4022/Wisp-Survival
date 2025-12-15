using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// InputSystem을 이용하여 구현
/// 입력 이벤트에 따라서 CharactorController_UI를 제어
/// </summary>
public class PlayerMove : IMoveable
{
    private IMoveable _moveable;
    private TouchControls _touchControl;
    private CharactorController_UI _charactorController;
    private SpriteRenderer _render;

    private InputAction.CallbackContext _context;
    private Coroutine _moving;
    private Vector3 _direction;
    private Vector2 _enterTouchPosition;
    private Vector2 _touchPosition;
    private bool _isPointerOverUI;
    private bool _active = false;
    private bool _didStartMove = false;

    public Vector2 Direction { get { return _direction; } }
    public float SpeedAmount { get { return Managers.Game.player.Stat.moveSpeed * SlowDownAmount * Time.deltaTime; } }
    public float SlowDownAmount { get { return _moveable.SlowDownAmount; } }
    public bool IsPointerOverUI { set { _isPointerOverUI = value; } }
    public void Init()
    {
        CoroutineHelper.Start(Initalization(), CoroutineType.Manage);
    }
    public void OnMove()
    {
        _touchPosition = _context.ReadValue<Vector2>();
        _direction = Default_Calculate.GetDirection(_touchPosition, _enterTouchPosition, false);
    }
    public void SetSlowDown(float slowDown, float duration)
    {
        _moveable.SetSlowDown(slowDown, duration);
    }
    public void SetDirection()
    {
        if(_direction.x > 0)
        {
            _render.flipX = false;
        }
        else if(_direction.x < 0)
        {
            _render.flipX = true;
        }
    }
    private void CancelMove()
    {
        Managers.UI.Hide<CharactorController_UI>();

        if(_moving != null)
        {
            CoroutineHelper.Stop(_moving);
        }

        _moving = null;
        _active = false;
        _didStartMove = false;

        Managers.Game.player.AnimationPlay("idle");
    }
    private IEnumerator Initalization()
    {
        _touchControl = Input_Manage.CreateAndGetInputAction<TouchControls>();

        _touchControl.Enable();

        yield return new WaitUntil(() => Managers.UI.Get<CharactorController_UI>() != null);

        _charactorController = Managers.UI.Get<CharactorController_UI>();

        _touchControl.Touch.TouchPress.started += (ctx =>
        {
            if(!_isPointerOverUI)
            {
                _active = true;
            }
        });

        _touchControl.Touch.TouchPress.canceled += (ctx =>
        {
            CancelMove();
        });

        _touchControl.Touch.TouchPosition.performed += (ctx =>
        {
            if(!_active)
            {
                return;
            }

            _context = ctx;

            if(!_didStartMove)
            {
                StartMove();
            }

            OnMove();
        });
    }
    private void StartMove()
    {
        Managers.UI.Show<CharactorController_UI>();

        _enterTouchPosition = _context.ReadValue<Vector2>();
        _charactorController.EnterPosition = _enterTouchPosition;
        _moving = CoroutineHelper.Start(Moving(), CoroutineType.Etc);
    }
    public PlayerMove(SpriteRenderer render, DefaultMoveable moveable)
    {
        _render = render;
        _moveable = moveable;
    }
    private IEnumerator Moving()
    {
        _didStartMove = true;

        Managers.Game.player.AnimationPlay("walk");

        while(true)
        {
            Managers.Game.player.gameObject.transform.position += _direction.normalized * SpeedAmount;

            SetDirection();
            _charactorController.SetJoyStick();

            yield return null;
        }
    }
}
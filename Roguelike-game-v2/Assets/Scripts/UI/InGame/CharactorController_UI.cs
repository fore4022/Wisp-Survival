using UnityEngine;
public class CharactorController_UI : UserInterface
{
    [SerializeField] private GameObject _stick;

    private Vector2 _enterPosition;

    private const int MaxLength = 85;

    public Vector2 EnterPosition
    {
        set
        {
            _enterPosition = value;

            transform.position = _enterPosition;
        }
    }
    public override void SetUserInterface()
    {
        Managers.UI.Hide<CharactorController_UI>();
    }
    public void SetJoyStick()
    {
        _stick.transform.position = _enterPosition + Vector2.ClampMagnitude(Managers.Game.player.move.Direction, MaxLength);
    }
}
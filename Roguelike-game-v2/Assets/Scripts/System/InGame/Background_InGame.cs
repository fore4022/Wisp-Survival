using UnityEngine;
/// <summary>
/// 인게임 무한 맵
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Background_InGame : MonoBehaviour
{
    private const float Width = 4.5f;
    private const float Height = 8;
    private const float PositionTolerance = 0.375f;

    private Vector3 _increasePos = new();
    private Vector2 _direction;
    private float _xPos;
    private float _yPos;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("View") || Managers.Game.player == null)
        {
            return;
        }

        _increasePos = new();
        _direction = Managers.Game.player.move.Direction;
        _xPos = Managers.Game.player.gameObject.transform.position.x;
        _yPos = Managers.Game.player.gameObject.transform.position.y;

        if(Mathf.Abs(Mathf.Abs(_xPos - transform.localPosition.x) - Width * 5) < PositionTolerance)
        {
            _increasePos.x += Mathf.Sign(_direction.x) * Width * 8;
        }

        if(Mathf.Abs(Mathf.Abs(_yPos - transform.localPosition.y) - Height * 5) < PositionTolerance)
        {
            _increasePos.y += Mathf.Sign(_direction.y) * Height * 8;
        }

        transform.position += _increasePos;
    }
} 
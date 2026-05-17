using UnityEngine;
/// <summary>
/// <para>
/// InGame Camera 구현
/// </para>
/// 플레이어를 따라서 이동, GameOver 카메라 연출
/// </summary>
public class InGameCamera : MonoBehaviour
{
    private GameObject _player = null;

    private const float ZPos = -10;

    private void Awake()
    {
        Managers.Game.restart += PositionUpdate;
    }
    private void Update()
    {
        if(_player == null)
        {
            if(Managers.Game.player != null)
            {
                _player = Managers.Game.player.gameObject;
            }
            else
            {
                return;
            }
        }

        if(!Managers.Game.player.Death)
        {
            if(Managers.Game.Playing)
            {
                PositionUpdate();
            }
        }
    }
    private void PositionUpdate()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, ZPos);
    }
}
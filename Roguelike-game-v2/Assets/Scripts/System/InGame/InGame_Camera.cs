using UnityEngine;
/// <summary>
/// <para>
/// InGame Camera 구현
/// </para>
/// 플레이어를 따라서 이동, GameOver 카메라 연출
/// </summary>
public class InGame_Camera : MonoBehaviour
{
    private GameObject player = null;

    private const float zpos = -10;

    private void Awake()
    {
        Managers.Game.restart += PositionUpdate;
    }
    private void Update()
    {
        if(player == null)
        {
            if(Managers.Game.player != null)
            {
                player = Managers.Game.player.gameObject;
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
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zpos);
    }
}
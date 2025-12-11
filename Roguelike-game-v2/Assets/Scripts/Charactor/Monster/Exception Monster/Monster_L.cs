using UnityEngine;
/// <summary>
/// 유효 회전 제한
/// </summary>
public class Monster_L : BasicMonster
{
    [SerializeField] private new float directionMultiplierDefault;

    protected override void Enable()
    {
        directionMultiplier = directionMultiplierDefault;

        base.Enable();
    }
}
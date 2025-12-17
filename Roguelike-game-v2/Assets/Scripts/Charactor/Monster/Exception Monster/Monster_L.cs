using UnityEngine;
/// <summary>
/// 유효 회전 제한
/// </summary>
/// <remarks>
/// 사용 객체 : BatG
/// </remarks>
public class Monster_L : BasicMonster
{
    [SerializeField] private float _directionMultiplierDefault;

    protected override void Enable()
    {
        _directionMultiplier = _directionMultiplierDefault;

        base.Enable();
    }
}
using UnityEngine;

/// <summary>
/// 유효 회전 제한
/// </summary>
/// <remarks>
/// 사용 객체 : BatG, DogB
/// </remarks>

public class MonsterL : BasicMonster
{
    [SerializeField] private float _directionMultiplierDefault;

    protected override void Enable()
    {
        _directionMultiplier = _directionMultiplierDefault;

        base.Enable();
    }
}
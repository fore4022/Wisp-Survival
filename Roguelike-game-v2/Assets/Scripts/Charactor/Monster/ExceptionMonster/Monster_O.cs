using UnityEngine;

/// <summary>
/// 잃은 체력에 비해서 능력치 상승
/// </summary>
/// <remarks>
/// 사용 객체 : CountA
/// </remarks>

public class Monster_O : BasicMonster
{
    [SerializeField] private Color _targetColor;
    [SerializeField] private float _multiplier;

    private float _healthLossRatio;
    
    protected override void Init()
    {
        base.Init();

        _onDamaged += HealthLossRatioUpdate;
        _onDamaged += ColorUpdate;
    }

    private void ColorUpdate()
    {
        _render.color = Color.Lerp(_defaultColor, _targetColor, _healthLossRatio);
    }

    private void HealthLossRatioUpdate()
    {
        _healthLossRatio = 1 - (_health / _maxHealth);
        _damageMultiplier = _speedMultiplier = Mathf.Max(1, _multiplier * _healthLossRatio);
    }
}
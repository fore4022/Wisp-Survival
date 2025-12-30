using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 활성화 될 때 지정된 색상, 범위 내에서 크기를 변경, 죽을 때 스킬을 시전
/// </summary>
/// <remarks>
/// 사용 객체 : FireSmallA
/// </remarks>
public class Monster_M : Monster_G
{
    [SerializeField] private List<Color> _colors;
    [SerializeField] private float _scaleValueMin;
    [SerializeField] private float _scaleValueMax;

    private float _value;

    protected override void Enable()
    {
        _value = Random.Range(_scaleValueMin, _scaleValueMax + 1);
        transform.localScale = new(_value, _value);
        _defaultColor = _colors[Random.Range(0, _colors.Count)];
     
        base.Enable();
    }
    protected override void SkillCast()
    {
        PoolingObject go = Managers.Game.objectPool.GetObject(_skillKey);

        _value /= 3;
        go.Transform.position = transform.position;
        go.Transform.localScale = new(_value, _value);
        go.SpriteRenderer.color = _render.color;

        go.SetActive(true);
    }
}
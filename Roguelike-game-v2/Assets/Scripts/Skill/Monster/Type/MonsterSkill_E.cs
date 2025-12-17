using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 스킬에 닿은 플레이어에게 피해를 주는 스킬
/// 고정된 위치에서 시전
/// 무작위 색상으로 시전
/// </summary>
/// <remarks>
/// 사용 객체 : F1
/// </remarks>
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSkill_E : MonsterSkill_D
{
    [SerializeField] private List<Color> _colors;
    [SerializeField] private Vector3 _skillOffset;

    protected override void Enable()
    {
        _render.color = _colors[Random.Range(0, _colors.Count)];

        base.Enable();
    }
}
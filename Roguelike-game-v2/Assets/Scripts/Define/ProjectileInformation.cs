using System;

/// <summary>
/// 스킬의 투사체 속성
/// </summary>

[Serializable]
public class ProjectileInformation
{
    public string animationName = "";
    public float speed;
    public int penetration = 0;
}
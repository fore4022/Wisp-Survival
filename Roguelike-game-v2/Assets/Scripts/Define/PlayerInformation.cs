using System.Collections.Generic;

/// <summary>
/// 플레이어 정보
/// </summary>

public class PlayerInformation
{
    public Dictionary<string, int> acquiredSkills = new();

    public DefaultStat stat = null;

    public float experienceForLevelUp;
    public float experience;
    public int level;
}
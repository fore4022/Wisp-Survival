/// <summary>
/// 플레이어 스킬의 정보와 Caster, 레벨을 포함하는 타입
/// </summary>

public class SkillContext
{
    public SkillInformation data;
    public SkillCaster caster = null;

    public int level = 0;

    public SkillContext(SkillInformationSO dataSO)
    {
        data = new(dataSO);
    }
}
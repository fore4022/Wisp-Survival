using System.Collections.Generic;
public class SkillCaster_Manage
{
    private Dictionary<string, SkillCaster> Casters = new();

    public void Reset()
    {
        Casters = new();
    }
    public SkillCaster GetCaster(string type)
    {
        if(Casters.ContainsKey(type))
        {
            return Casters[type];
        }

        return default;
    }
    public SkillCaster CreateAndGetCaster(string type)
    {
        if(!Casters.ContainsKey(type))
        {
            SkillCaster caster = new();

            caster.SetAttackType(type);
            Casters.Add(type, caster);

            return Casters[type];
        }

        return default;
    }
    public void UpdateCasterLevel(string type, int level)
    {
        Casters[type].Level = level;
    }
    public void DestroyCaster(string type)
    {
        if(Casters.ContainsKey(type))
        {
            Casters[type] = null;

            Casters.Remove(type);
        }
    }
    public void StopAllCaster()
    {
        foreach(SkillCaster caster in Casters.Values)
        {
            caster.CastingStop();
        }
    }
}
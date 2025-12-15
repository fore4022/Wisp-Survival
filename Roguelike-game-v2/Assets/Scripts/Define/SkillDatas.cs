using System.Collections.Generic;
using System.Linq;
/// <summary>
/// 스킬의 정보를 관리하는 역할
/// </summary>
public class SkillDatas
{
    private Dictionary<string, SkillContext> _infos = new();

    public void Reset()
    {
        foreach(string key in _infos.Keys)
        {
            _infos[key].caster = null;
        }
    }
    public void SetDictionaryItem(SkillInformation_SO so)
    {
        if(!_infos.ContainsKey(so.Info.type))
        {
            _infos.Add(so.Info.type, new SkillContext(so));
        }
    }
    public void SetValue(string key, int levelDelta = 1)
    {
        if(TryGetSkillData(key, out SkillContext info))
        {
            if(info.caster == null)
            {
                info.caster = Managers.Game.skillCaster_Manage.CreateAndGetCaster(key);
                info.caster.Level = levelDelta - 1;
            }
            else
            {
                info.level += levelDelta;

                Managers.Game.skillCaster_Manage.UpdateCasterLevel(key, info.level);
            }
        }
    }
    public int GetLevel(string key)
    {
        if(TryGetSkillData(key, out SkillContext info))
        {
            return info.level;
        }

        return -1;
    }
    public List<SkillContext> GetSkill_Information()
    {
        List<SkillContext> info = _infos.Values.ToList();

        info.RemoveAll(o => o.level == Skill_SO.MaxLevel - 1);

        return info;
    }
    private bool TryGetSkillData(string key, out SkillContext info)
    {
        if(_infos.ContainsKey(key))
        {
            info = _infos[key];

            return true;
        }
        else
        {
            info = null;

            return false;
        }
    }
}
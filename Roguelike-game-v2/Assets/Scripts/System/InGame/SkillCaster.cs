using System.Collections;
using UnityEngine;
public class SkillCaster
{
    private Skill_SO _so = null;
    private WaitForSeconds _coolTime;
    private WaitForSeconds _delay;

    private Coroutine _cast;
    private string _attackType;
    private int _level;

    public int Level 
    {
        set
        {
            _level = value;

            Set();
        }
    }
    public void SetAttackType(string attackType)
    {
        _attackType = attackType;

        _cast = CoroutineHelper.Start(Casting(), CoroutineType.InGameSystem);
    }
    private void Set()
    {
        _coolTime = new(_so.CoolTime[_level]);

        if(_so.IsMultiCast)
        {
            _delay = new(_so.MultiCast_Info.delay[_level]);
        }
    }
    public void CastingStop()
    {
        CoroutineHelper.Stop(_cast);
    }
    private IEnumerator Casting()
    {
        _so = Managers.Game.so_Manage.GetScriptableObject<Skill_SO>(_attackType);

        yield return new WaitUntil(() => _so != null);

        Set();

        if(!_so.IsMultiCast)
        {
            while (true)
            {
                yield return _coolTime;

                Managers.Game.objectPool.ActiveObject(_attackType);
            }
        }
        else
        {
            int i;

            while(true)
            {
                yield return _coolTime;

                for(i = 0; i < _so.MultiCast_Info.count[_level]; i++)
                {
                    Managers.Game.objectPool.ActiveObject(_attackType);

                    yield return _delay;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 몬스터, 스킬의 ScriptableObject 참조
/// </summary>
public class PoolingObject_Initializer
{
    private List<PoolingObject> _objs;
    private ScriptableObject _so;

    private Coroutine _coroutine = null;
    private string _key;
    private bool _isInit = false;

    public bool Init { get { return _isInit; } }
    public void Start(List<GameObject> monsterList, List<GameObject> skillList)
    {
        CoroutineHelper.Start(Initializing(monsterList, skillList), CoroutineType.Manage);
    }
    private IEnumerator Initializing(List<GameObject> monsterList, List<GameObject> skillList)
    {
        _coroutine = CoroutineHelper.Start(Set_MonsterList(monsterList), CoroutineType.InGameSystem);

        yield return null;

        yield return new WaitUntil(() => _coroutine == null);

        _coroutine = CoroutineHelper.Start(Set_SkillList(skillList), CoroutineType.InGameSystem);
        
        yield return null;

        yield return new WaitUntil(() => _coroutine == null);

        _isInit = true;
    }
    private IEnumerator Set_MonsterList(List<GameObject> monsterList)
    {
        foreach(GameObject obj in monsterList)
        {
            _key = obj.name;
            _objs = Managers.Game.objectPool.GetObjects(_key);

            Task load = Managers.Game.so_Manage.LoadScriptableObject(ScriptableObjectType.Monster, _key);

            yield return new WaitUntil(() => load.IsCompleted);

            CoroutineHelper.Start(Managers.Game.so_Manage.SetScriptableObject(_objs, _key), CoroutineType.Manage);

            _so = Managers.Game.so_Manage.GetScriptableObject<ScriptableObject>(_key);

            if(_so is MonsterStat_WithObjectSO exceptionMonsterStatSO)
            {
                if(exceptionMonsterStatSO.ExtraObjects != null)
                {
                    foreach(GameObject extraObj in exceptionMonsterStatSO.ExtraObjects)
                    {
                        if(!Managers.Game.objectPool.PoolingObjects.ContainsKey(extraObj.name))
                        {
                            CoroutineHelper.Start(CreateAndSet_ExtraObject(extraObj, _key), CoroutineType.Manage);
                        }
                    }
                }
            }
        }

        _coroutine = null;
    }
    private IEnumerator Set_SkillList(List<GameObject> skillList)
    {
        foreach(GameObject obj in skillList)
        {
            _key = obj.name;
            _objs = Managers.Game.objectPool.GetObjects(_key);

            Task load = Managers.Game.so_Manage.LoadScriptableObject(ScriptableObjectType.Skill, _key);

            yield return new WaitUntil(() => load.IsCompleted);

            CoroutineHelper.Start(Managers.Game.so_Manage.SetScriptableObject(_objs, _key), CoroutineType.Manage);
        }

        _coroutine = null;
    }
    private IEnumerator CreateAndSet_ExtraObject(GameObject extraObj, string key)
    {
        string key_extra = extraObj.name;

        Managers.Game.objectPool.Create(extraObj);

        yield return new WaitUntil(() => Managers.Game.objectPool.PoolingObjects.ContainsKey(key_extra));

        if(extraObj.GetComponent<MonsterSkillDamage>())
        {
            Monster monster = Managers.Game.objectPool.GetObject(key, false).GetComponent<Monster>();
            MonsterSkillDamage skillDamage;

            foreach(PoolingObject poolingObj in Managers.Game.objectPool.PoolingObjects[key_extra])
            {
                skillDamage = poolingObj.GetComponent<MonsterSkillDamage>();
                skillDamage.Damage += monster.Damage;
            }
        }
    }
}
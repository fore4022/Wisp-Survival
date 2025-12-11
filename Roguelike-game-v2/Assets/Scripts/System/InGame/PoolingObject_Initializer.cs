using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 몬스터, 스킬의 ScriptableObject 참조
/// </summary>
public class PoolingObject_Initializer
{
    private List<PoolingObject> objs;
    private ScriptableObject so;

    private Coroutine coroutine = null;
    private string key;
    private bool isInit = false;

    public bool Init { get { return isInit; } }
    public void Start(List<GameObject> monsterList, List<GameObject> skillList)
    {
        CoroutineHelper.Start(Initializing(monsterList, skillList), CoroutineType.Manage);
    }
    private IEnumerator Initializing(List<GameObject> monsterList, List<GameObject> skillList)
    {
        coroutine = CoroutineHelper.Start(Set_MonsterList(monsterList));

        yield return null;

        yield return new WaitUntil(() => coroutine == null);

        coroutine = CoroutineHelper.Start(Set_SkillList(skillList));
        
        yield return null;

        yield return new WaitUntil(() => coroutine == null);

        isInit = true;
    }
    private IEnumerator Set_MonsterList(List<GameObject> monsterList)
    {
        foreach(GameObject obj in monsterList)
        {
            key = obj.name;
            objs = Managers.Game.objectPool.GetObjects(key);

            Task load = Managers.Game.so_Manage.LoadScriptableObject(ScriptableObjectType.Monster, key);

            yield return new WaitUntil(() => load.IsCompleted);

            CoroutineHelper.Start(Managers.Game.so_Manage.SetScriptableObject(objs, key), CoroutineType.Manage);

            so = Managers.Game.so_Manage.GetScriptableObject<ScriptableObject>(key);

            if(so is MonsterStat_WithObject_SO exceptionMonsterStatSO)
            {
                if(exceptionMonsterStatSO.ExtraObjects != null)
                {
                    foreach(GameObject extraObj in exceptionMonsterStatSO.ExtraObjects)
                    {
                        if(!Managers.Game.objectPool.PoolingObjects.ContainsKey(extraObj.name))
                        {
                            CoroutineHelper.Start(CreateAndSet_ExtraObject(extraObj, key), CoroutineType.Manage);
                        }
                    }
                }
            }
        }

        coroutine = null;
    }
    private IEnumerator Set_SkillList(List<GameObject> skillList)
    {
        foreach(GameObject obj in skillList)
        {
            key = obj.name;
            objs = Managers.Game.objectPool.GetObjects(key);

            Task load = Managers.Game.so_Manage.LoadScriptableObject(ScriptableObjectType.Skill, key);

            yield return new WaitUntil(() => load.IsCompleted);

            CoroutineHelper.Start(Managers.Game.so_Manage.SetScriptableObject(objs, key), CoroutineType.Manage);
        }

        coroutine = null;
    }
    private IEnumerator CreateAndSet_ExtraObject(GameObject extraObj, string key)
    {
        string key_extra = extraObj.name;

        Managers.Game.objectPool.Create(extraObj);

        yield return new WaitUntil(() => Managers.Game.objectPool.PoolingObjects.ContainsKey(key_extra));

        if(extraObj.GetComponent<MonsterSkill_Damage>())
        {
            Monster monster = Managers.Game.objectPool.GetObject(key, false).GetComponent<Monster>();
            MonsterSkill_Damage skillDamage;

            foreach(PoolingObject poolingObj in Managers.Game.objectPool.PoolingObjects[key_extra])
            {
                skillDamage = poolingObj.GetComponent<MonsterSkill_Damage>();
                skillDamage.Damage += monster.Damage;
            }
        }
    }
}
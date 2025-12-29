using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 몬스터를 주기적으로 생성
/// </summary>
public class MonsterSpawner
{
    [HideInInspector] public List<GameObject> monsterList = new();

    private Dictionary<string, ScriptableObject> _monsterStats = new();

    private const float MinimumSpawnDelay = 0.05f;

    private Coroutine _monsterSpawn = null;
    private Coroutine _spawnGroup = null;
    private int[] _monsterSpawnProbabilityArray = new int[100];
    private float _spawnDelay = 0;

    public MonsterSpawner()
    {
        Managers.Game.monsterSpawner = this;
    }
    public void StartSpawn()
    {
        _monsterSpawn = CoroutineHelper.Start(SpawningSystem(), CoroutineType.InGameSystem);
    }
    public void StopSpawn()
    {
        CoroutineHelper.Stop(_monsterSpawn, CoroutineType.InGameSystem);
        CoroutineHelper.Stop(_spawnGroup, CoroutineType.InGameSystem);
    }
    public void ReStart()
    {
        if(_spawnGroup != null)
        {
            CoroutineHelper.Stop(_spawnGroup, CoroutineType.InGameSystem);
        }

        if(_monsterSpawn != null)
        {
            CoroutineHelper.Stop(_monsterSpawn, CoroutineType.InGameSystem);
        }

        StartSpawn();
    }
    private void LoadInformation()
    {
        foreach(GameObject monster in monsterList)
        {
            string soName = monster.name;

            if(!_monsterStats.ContainsKey(soName))
            {
                _monsterStats.Add(soName, Managers.Game.so_Manage.GetScriptableObject<ScriptableObject>(soName));
            }
        }
    }
    private void MonsterSpawn(SpawnPattern_SO spawnInformation) 
    {
        int arrayIndexValue = _monsterSpawnProbabilityArray[Random.Range(0, 100)];

        Managers.Game.objectPool.ActiveObject(spawnInformation.MonsterInformation[arrayIndexValue].monster.name);
    }
    private IEnumerator SpawningSystem()
    {
        LoadInformation();

        while(!Managers.Game.GameOver)
        {
            foreach(SpawnPattern_SO spawnInformation in Managers.Game.stageInformation.SpawnPatternList.Patterns)
            {
                _spawnGroup = CoroutineHelper.Start(MonsterSpawning(spawnInformation), CoroutineType.InGameSystem);

                yield return new WaitUntil(() => _spawnGroup == null);
            }
        }

        if(_spawnGroup != null)
        {
            CoroutineHelper.Stop(_spawnGroup, CoroutineType.InGameSystem);
        }
    }
    private IEnumerator MonsterSpawning(SpawnPattern_SO spawnInformation)
    {
        int totalMinutes = Managers.Game.inGameTimer.GetTotalMinutes;
        int index = 0;

        foreach(SpawnPattern_Information spawnInfo in spawnInformation.MonsterInformation)
        {
            for(int i = 0; i < spawnInfo.spawnProbability; i++)
            {
                _monsterSpawnProbabilityArray[i] = index;
            }

            index++;
        }

        _spawnDelay = Managers.Game.difficultyScaler.SpawnDelay;

        while(Managers.Game.inGameTimer.GetTotalMinutes < totalMinutes + spawnInformation.Duration)
        {
            if(_spawnDelay != MinimumSpawnDelay)
            {
                _spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, MinimumSpawnDelay);
            }

            MonsterSpawn(spawnInformation);

            yield return new WaitForSeconds(_spawnDelay);
        }

        _spawnGroup = null;
    }
}
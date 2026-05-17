using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 자신의 주변에 몬스터를 소환
/// </summary>
/// <remarks>
/// 사용 객체 : WitchE
/// </remarks>

public class MonsterI : BasicMonster_WithObject
{
    [SerializeField] private List<Vector3> _skillOffset;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _coolTime;
    [SerializeField] private float _monsterCount;

    private Coroutine _behavior = null;
    private WaitForSeconds _waitSpawnDelay;
    private WaitForSeconds _waitCoolTime;
    private string _monsterKey;

    protected override void Init()
    {
        _waitSpawnDelay = new(_spawnDelay);
        _waitCoolTime = new(_coolTime);
        _monsterKey = monsterSO.ExtraObjects[0].name;

        base.Init();
    }

    protected override void Enable()
    {
        base.Enable();

        _behavior = StartCoroutine(RepeatBehavior());
    }

    protected override void Die()
    {
        base.Die();

        StopCoroutine(_behavior);
    }

    private IEnumerator RepeatBehavior()
    {
        if(Random.Range(0, 2) == 1)
        {
            yield return _waitCoolTime;
        }

        PoolingObject go;

        while(true)
        {
            for(int i = 0; i < _monsterCount; i++)
            {
                go = Managers.Game.objectPool.GetObject(_monsterKey);
                go.Transform.position = transform.position + _skillOffset[Random.Range(1, _skillOffset.Count)];
                
                go.SetActive(true);

                yield return _spawnDelay;
            }

            yield return _waitCoolTime;
        }
    }
}
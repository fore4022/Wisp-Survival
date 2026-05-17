using UnityEngine;

/// <summary>
/// 몬스터 종류, 종류에 해당하는 생성 확률을 포함한 타입
/// </summary>

[System.Serializable]
public class SpawnPatternInformation
{
    public GameObject monster;

    public float spawnProbability;
}
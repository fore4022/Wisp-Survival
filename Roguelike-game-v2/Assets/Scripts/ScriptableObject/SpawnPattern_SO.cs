using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnPattern", menuName = "Create New SO/Game Stage/Create New SpawnPattern_SO")]
public class SpawnPattern_SO : ScriptableObject
{
    [SerializeField] private List<SpawnPattern_Information> monsterInformation;

    [SerializeField] private int duration;

    public List<SpawnPattern_Information> MonsterInformation { get { return monsterInformation; } }
    public int Duration { get { return duration; } }
}
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnPattern", menuName = "Create New SO/Game Stage/Create New SpawnPattern_SO")]
public class SpawnPatternSO : ScriptableObject
{
    [SerializeField] private List<SpawnPatternInformation> _monsterInformation;

    [SerializeField] private int _duration;

    public List<SpawnPatternInformation> MonsterInformation { get { return _monsterInformation; } }
    public int Duration { get { return _duration; } }
}
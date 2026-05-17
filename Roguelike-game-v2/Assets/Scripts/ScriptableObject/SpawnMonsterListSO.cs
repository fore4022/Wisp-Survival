using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnMonsterList", menuName = "Create New SO/Game Stage/Create New SpawnMonsterList_SO")]
public class SpawnMonsterListSO : ScriptableObject
{
    [SerializeField] private List<GameObject> _monsters;

    public List<GameObject> Monsters { get { return _monsters; } }
}
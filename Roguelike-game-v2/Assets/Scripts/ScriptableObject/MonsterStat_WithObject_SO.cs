using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MonsterStat_WithObject", menuName = "Create New SO/Monster/Create New MonsterStat_WithObject_SO")]
public class MonsterStat_WithObject_SO : MonsterStat_SO
{
    [SerializeField] private List<GameObject> extraObjects;

    [SerializeField] private bool hasExtraObject;

    public List<GameObject> ExtraObjects { get { return extraObjects; } }
    public bool HasExtraObject { get { return hasExtraObject; } }
}
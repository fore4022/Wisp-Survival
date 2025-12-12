using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MonsterStat_WithObject", menuName = "Create New SO/Monster/Create New MonsterStat_WithObject_SO")]
public class MonsterStat_WithObject_SO : MonsterStat_SO
{
    [SerializeField] private List<GameObject> _extraObjects;

    [SerializeField] private bool _hasExtraObject;

    public List<GameObject> ExtraObjects { get { return _extraObjects; } }
    public bool HasExtraObject { get { return _hasExtraObject; } }
}
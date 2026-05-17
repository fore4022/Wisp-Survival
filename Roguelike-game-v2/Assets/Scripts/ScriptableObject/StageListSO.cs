using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageList", menuName = "Create New SO/Game Stage/Create New StageList_SO")]
public class StageListSO : ScriptableObject
{
    [SerializeField] private List<StageSO> _stageList;

    public List<StageSO> StageList { get { return _stageList; } }
}
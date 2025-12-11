using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageList", menuName = "Create New SO/Game Stage/Create New StageList_SO")]
public class StageList_SO : ScriptableObject
{
    [SerializeField] private List<Stage_SO> stageList;

    public List<Stage_SO> StageList { get { return stageList; } }
}
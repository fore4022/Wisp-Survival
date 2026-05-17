using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevel", menuName = "Create New SO/User Level/Create New UserLevel_SO")]
public class UserLevelSO : ScriptableObject
{
    [SerializeField] private List<SkillInformationSO> _skillInformationList;

    public List<SkillInformationSO> SkillInformationList { get { return _skillInformationList; } }
}
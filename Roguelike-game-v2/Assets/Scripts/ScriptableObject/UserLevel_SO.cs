using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevel", menuName = "Create New SO/User Level/Create New UserLevel_SO")]
public class UserLevel_SO : ScriptableObject
{
    [SerializeField] private List<SkillInformation_SO> _skillInformationList;

    public List<SkillInformation_SO> SkillInformationList { get { return _skillInformationList; } }
}
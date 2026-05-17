using UnityEngine;
[CreateAssetMenu(fileName = "SkillInformation", menuName = "Create New SO/Skill/Create New SkillInformation_SO")]
public class SkillInformationSO : ScriptableObject
{
    [SerializeField] private SkillInformation _info;
    [SerializeField] private Sprite _icon;

    public SkillInformation Info { get { return _info; } }
    public Sprite Icon { get { return _icon; } }
}
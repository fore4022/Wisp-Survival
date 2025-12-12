using UnityEngine;
[CreateAssetMenu(fileName = "SkillInformation", menuName = "Create New SO/Skill/Create New SkillInformation_SO")]
public class SkillInformation_SO : ScriptableObject
{
    [SerializeField] private Skill_Information _info;
    [SerializeField] private Sprite _icon;

    public Skill_Information Info { get { return _info; } }
    public Sprite Icon { get { return _icon; } }
}
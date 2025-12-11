using UnityEngine;
[CreateAssetMenu(fileName = "SkillInformation", menuName = "Create New SO/Skill/Create New SkillInformation_SO")]
public class SkillInformation_SO : ScriptableObject
{
    [SerializeField] private Skill_Information info;
    [SerializeField] private Sprite icon;

    public Skill_Information Info { get { return info; } }
    public Sprite Icon { get { return icon; } }
}
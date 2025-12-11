using UnityEngine;
/// <summary>
/// 플레이어 스킬의 정보 타입
/// </summary>
[System.Serializable]
public class Skill_Information
{
    [HideInInspector] public Sprite icon;

    public GameObject go;

    public string name;
    public string explanation;

    public Skill_Information(SkillInformation_SO so)
    {
        icon = so.Icon;
        go = so.Info.go;
        name = so.Info.name;
        explanation = so.Info.explanation;
    }
    public string type { get { return go.name; } }
}
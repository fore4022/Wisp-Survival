using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevel", menuName = "Create New SO/User Level/Create New UserLevel_SO")]
public class UserLevel_SO : ScriptableObject
{
    [SerializeField][ReadOnly] private List<string> pathList;

    public List<string> PathList { get { return pathList; } }

#if UNITY_EDITOR
    public List<SkillInformation_SO> skillInformationList;

    private string path;

    private int count { get { return skillInformationList.Count; } }
    private void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(pathList.Count != skillInformationList.Count)
            {
                Validate();
            }
            else
            {
                ValidateUntilReady();
            }
        };
    }
    private void Validate()
    {
        if(count < skillInformationList.Count)
        {
            for(int i = count; i < skillInformationList.Count; i++)
            {
                path = $"Assets/SO/SkillInformation/{skillInformationList[i].name}.asset";

                pathList.Add(path);
            }
        }
        else
        {
            pathList = new();

            foreach(SkillInformation_SO so in skillInformationList)
            {
                path = $"Assets/SO/SkillInformation/{so.name}.asset";

                pathList.Add(path);
            }
        }
    }
#endif
}
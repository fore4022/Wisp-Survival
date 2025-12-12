using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "UserLevel", menuName = "Create New SO/User Level/Create New UserLevel_SO")]
public class UserLevel_SO : ScriptableObject
{
    [SerializeField][ReadOnly] private List<string> _pathList;

    public List<string> PathList { get { return _pathList; } }

#if UNITY_EDITOR
    public List<SkillInformation_SO> skillInformationList;

    private string _path;

    private int count { get { return skillInformationList.Count; } }
    private void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(_pathList.Count != skillInformationList.Count)
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
                _path = $"Assets/SO/SkillInformation/{skillInformationList[i].name}.asset";

                _pathList.Add(_path);
            }
        }
        else
        {
            _pathList = new();

            foreach(SkillInformation_SO so in skillInformationList)
            {
                _path = $"Assets/SO/SkillInformation/{so.name}.asset";

                _pathList.Add(_path);
            }
        }
    }
#endif
}
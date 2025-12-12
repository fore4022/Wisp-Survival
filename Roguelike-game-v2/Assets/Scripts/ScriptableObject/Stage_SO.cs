using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Game Stage/Create New Stage_SO")]
public class Stage_SO : ScriptableObject
{
    [SerializeField][HideInInspector] private string _infoPath;
    [SerializeField][HideInInspector] private string _iconPath;

    [SerializeField] private string _stagePath;
    [SerializeField] private string _name;

    public string InfoPath { get { return _infoPath; } }
    public string IconPath { get { return _iconPath; } }
    public string StagePath { get { return _stagePath; } }
    public string Name { get { return _name; } }

#if UNITY_EDITOR
    public StageInformation_SO information = null;
    public Icon_SO iconSprite = null;

    private void OnValidate()
    {
        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if(iconSprite == null || information == null)
            {
                ValidateUntilReady();
            }
            else
            {
                Validate();
            }
        };
    }
    private void Validate()
    {
        _infoPath = information.name;
        _iconPath = iconSprite.name;
    }
#endif
}
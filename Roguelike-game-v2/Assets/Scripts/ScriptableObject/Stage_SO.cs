using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Game Stage/Create New Stage_SO")]
public class Stage_SO : ScriptableObject
{
    [SerializeField][HideInInspector] private string infoPath;
    [SerializeField][HideInInspector] private string iconPath;

    [SerializeField] private string stagePath;
    [SerializeField] private new string name;

    public string InfoPath { get { return infoPath; } }
    public string IconPath { get { return iconPath; } }
    public string StagePath { get { return stagePath; } }
    public string Name { get { return name; } }

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
        infoPath = information.name;
        iconPath = iconSprite.name;
    }
#endif
}
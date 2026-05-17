using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Game Stage/Create New Stage_SO")]
public class StageSO : ScriptableObject
{
    [SerializeField] private StageInformation_SO _information = null;
    [SerializeField] private IconSO _iconSprite = null;

    [SerializeField] private string _stagePath;
    [SerializeField] private string _name;

    public StageInformation_SO Information { get { return _information; } }
    public IconSO IconSprite { get { return _iconSprite; } }
    public string StagePath { get { return _stagePath; } }
    public string Name { get { return _name; } }
}
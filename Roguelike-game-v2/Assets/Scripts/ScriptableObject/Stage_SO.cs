using UnityEngine;
[CreateAssetMenu(fileName = "Stage", menuName = "Create New SO/Game Stage/Create New Stage_SO")]
public class Stage_SO : ScriptableObject
{
    [SerializeField] private StageInformation_SO _information = null;
    [SerializeField] private Icon_SO _iconSprite = null;

    [SerializeField] private string _stagePath;
    [SerializeField] private string _name;

    public StageInformation_SO Information { get { return _information; } }
    public Icon_SO IconSprite { get { return _iconSprite; } }
    public string StagePath { get { return _stagePath; } }
    public string Name { get { return _name; } }
}
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Create New SO/Skill/Create New Skill_SO")]
public class Skill_SO : ScriptableObject
{
    public const int maxLevel = 5;

    [SerializeField] private Projectile_Information projectile_Info;
    [SerializeField] private MultiCast multiCast_Info;


    [SerializeField] private Color maxLevelColor = default;
    [SerializeField] private Vector3 adjustmentRotation;
    [SerializeField] private Vector2 adjustmentPosition;
    [SerializeField] private string typePath;
    [SerializeField] private float[] damageCoefficient = new float[maxLevel];
    [SerializeField] private float[] coolTime = new float[maxLevel];
    [SerializeField] private float duration;
    [SerializeField] private bool flipX = false;
    [SerializeField] private bool flipY = false;
    [SerializeField] private bool isProjectile;
    [SerializeField] private bool isMultiCast;

    public Projectile_Information Projectile_Info { get { return projectile_Info; } }
    public MultiCast MultiCast_Info { get { return multiCast_Info; } }
    public Color MaxLevelColor { get { return maxLevelColor; } }
    public Vector3 AdjustmentRotation { get { return adjustmentRotation; } }
    public Vector2 AdjustmentPosition { get { return adjustmentPosition; } }
    public string TypePath { get { return typePath; } }
    public float[] DamageCoefficient { get { return damageCoefficient; } }
    public float[] CoolTime { get { return coolTime; } }
    public float Duration { get { return duration; } }
    public bool FlipX { get { return flipX; } }
    public bool FlipY { get { return flipY; } }
    public bool IsProjectile { get { return isProjectile; } }
    public bool IsMultiCast { get { return isMultiCast; } }

#if UNITY_EDITOR
    public GameObject go;

    private void OnValidate()
    {
        ArrayUtil.ResizeArray(ref damageCoefficient, maxLevel);
        ArrayUtil.ResizeArray(ref coolTime, maxLevel);

        if(isMultiCast)
        {
            ArrayUtil.ResizeArray(ref multiCast_Info.delay, maxLevel);
            ArrayUtil.ResizeArray(ref multiCast_Info.count, maxLevel);
        }

        ValidateUntilReady();
    }
    private void ValidateUntilReady()
    {
        EditorApplication.delayCall += () =>
        {
            if (go == null)
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
        typePath = go.name;
    }
#endif
}
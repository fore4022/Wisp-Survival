using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Create New SO/Skill/Create New Skill_SO")]
public class SkillSO : ScriptableObject
{
    public const int MaxLevel = 5;

    [SerializeField] private ProjectileInformation _projectileInfo;
    [SerializeField] private MultiCast _multiCastInfo;


    [SerializeField] private Color _maxLevelColor = default;
    [SerializeField] private Vector3 _adjustmentRotation;
    [SerializeField] private Vector2 _adjustmentPosition;
    [SerializeField] private string _typePath;
    [SerializeField] private float[] _damageCoefficient = new float[MaxLevel];
    [SerializeField] private float[] _coolTime = new float[MaxLevel];
    [SerializeField] private float _duration;
    [SerializeField] private bool _flipX = false;
    [SerializeField] private bool _flipY = false;
    [SerializeField] private bool _isProjectile;
    [SerializeField] private bool _isMultiCast;

    public ProjectileInformation Projectile_Info { get { return _projectileInfo; } }
    public MultiCast MultiCast_Info { get { return _multiCastInfo; } }
    public Color MaxLevelColor { get { return _maxLevelColor; } }
    public Vector3 AdjustmentRotation { get { return _adjustmentRotation; } }
    public Vector2 AdjustmentPosition { get { return _adjustmentPosition; } }
    public string TypePath { get { return _typePath; } }
    public float[] DamageCoefficient { get { return _damageCoefficient; } }
    public float[] CoolTime { get { return _coolTime; } }
    public float Duration { get { return _duration; } }
    public bool FlipX { get { return _flipX; } }
    public bool FlipY { get { return _flipY; } }
    public bool IsProjectile { get { return _isProjectile; } }
    public bool IsMultiCast { get { return _isMultiCast; } }

#if UNITY_EDITOR
    public GameObject go;

    private void OnValidate()
    {
        ArrayUtil.ResizeArray(ref _damageCoefficient, MaxLevel);
        ArrayUtil.ResizeArray(ref _coolTime, MaxLevel);

        if(_isMultiCast)
        {
            ArrayUtil.ResizeArray(ref _multiCastInfo.delay, MaxLevel);
            ArrayUtil.ResizeArray(ref _multiCastInfo.count, MaxLevel);
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
        _typePath = go.name;
    }
#endif
}
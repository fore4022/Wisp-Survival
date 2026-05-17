using UnityEngine;
[CreateAssetMenu(fileName = "StageInformation", menuName = "Create New SO/Game Stage/Create New StageInformation_SO")]
public class StageInformation_SO : ScriptableObject
{
    [SerializeField] private SpawnPatternList_SO _spawnPatternList;
    [SerializeField] private SpawnMonsterListSO _spawnMonsterList;
    [SerializeField] private AudioClip _bgm;

    [SerializeField][Tooltip("Damage Text")] private Color _damageTextColor = new(255, 255, 255, 255);
    [SerializeField] private Color _skillRangeVisualizerColor = new(255, 50, 50);
    [SerializeField] private float _difficulty = 1;
    [SerializeField] private float _statScale = 1;
    [SerializeField] private float _spawnDelay;
    [SerializeField][Tooltip("Minute")] private int _requiredTime;

    public SpawnPatternList_SO SpawnPatternList { get { return _spawnPatternList; } }
    public SpawnMonsterListSO SpawnMonsterList { get { return _spawnMonsterList; } }
    public AudioClip BGM { get { return _bgm; } }
    public Color DamageTextColor { get { return _damageTextColor; } }
    public Color SkillRangeVisualizerColor { get { return _skillRangeVisualizerColor; } }
    public float Difficulty { get { return _difficulty; } }
    public float StatScale { get { return _statScale; } }
    public float SpawnDelay { get { return _spawnDelay; } }
    public int RequiredTime { get { return _requiredTime; } }

#if UNITY_EDITOR
    [Tooltip("Skill Range VisualizerColor")] public bool isDefaultColor = true;

    private int _defaultAlpha = 200;

    private void OnValidate()
    {
        if (isDefaultColor)
        {
            _skillRangeVisualizerColor = new Color32(255, 50, 50, (byte)_defaultAlpha);
        }
        else
        {
            _skillRangeVisualizerColor = new(_skillRangeVisualizerColor.r, _skillRangeVisualizerColor.g, _skillRangeVisualizerColor.b, _defaultAlpha / 255f);
        }
    }
#endif
}
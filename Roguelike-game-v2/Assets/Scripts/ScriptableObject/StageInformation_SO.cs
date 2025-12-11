using UnityEngine;
[CreateAssetMenu(fileName = "StageInformation", menuName = "Create New SO/Game Stage/Create New StageInformation_SO")]
public class StageInformation_SO : ScriptableObject
{
    [SerializeField] private SpawnPatternList_SO spawnPatternList;
    [SerializeField] private SpawnMonsterList_SO spawnMonsterList;
    [SerializeField] private AudioClip bgm;

    [SerializeField][Tooltip("Damage Text")] private Color damageTextColor = new(255, 255, 255, 255);
    [SerializeField] private Color skillRangeVisualizerColor = new(255, 50, 50);
    [SerializeField] private float difficulty = 1;
    [SerializeField] private float statScale = 1;
    [SerializeField] private float spawnDelay;
    [SerializeField][Tooltip("Minute")] private int requiredTime;

    public SpawnPatternList_SO SpawnPatternList { get { return spawnPatternList; } }
    public SpawnMonsterList_SO SpawnMonsterList { get { return SpawnMonsterList; } }
    public AudioClip BGM { get { return bgm; } }
    public Color DamageTextColor { get { return damageTextColor; } }
    public Color SkillRangeVisualizerColor { get { return skillRangeVisualizerColor; } }
    public float Difficulty { get { return difficulty; } }
    public float StatScale { get { return statScale; } }
    public float SpawnDelay { get { return spawnDelay; } }
    public int RequiredTime { get { return requiredTime; } }

#if UNITY_EDITOR
    [Tooltip("Skill Range VisualizerColor")] public bool isDefaultColor = true;

    private int defaultAlpha = 200;

    private void OnValidate()
    {
        if (isDefaultColor)
        {
            skillRangeVisualizerColor = new Color32(255, 50, 50, (byte)defaultAlpha);
        }
        else
        {
            skillRangeVisualizerColor = new(skillRangeVisualizerColor.r, skillRangeVisualizerColor.g, skillRangeVisualizerColor.b, defaultAlpha / 255f);
        }
    }
#endif
}
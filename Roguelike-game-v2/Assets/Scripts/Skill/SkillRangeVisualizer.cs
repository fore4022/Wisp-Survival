using UnityEngine;
public class SkillRangeVisualizer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Managers.Game.stageInformation.SkillRangeVisualizerColor;
    }
}
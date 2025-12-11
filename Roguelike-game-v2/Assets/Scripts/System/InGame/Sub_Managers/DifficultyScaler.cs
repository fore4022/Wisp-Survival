using UnityEngine;
/// <summary>
/// 시간에 따른 난이도를 조절하는 시스템
/// </summary>
public class DifficultyScaler
{
    private const float increaseRate = 0.085f;
    private const int criticalMinute = 8;

    private float minute;

    public float SpawnDelay { get { return Mathf.Lerp(1, 0.33f, (Managers.Game.inGameTimer.GetSeconds / 60f)) * Managers.Game.stageInformation.SpawnDelay / GetDifficultyScale(); } }
    public float IncreaseStat { get { return Managers.Game.stageInformation.StatScale * GetDifficultyScale(); } }
    private float GetDifficultyScale()
    {
        minute = Managers.Game.inGameTimer.GetMinutes;
        
        return (1 + increaseRate * (Managers.Game.stageInformation.Difficulty - 1)) * (1 + (increaseRate * (1 + ((Managers.Game.stageInformation.Difficulty - 1) / 10))) * minute + (minute > criticalMinute ? 0.001f * Mathf.Pow(minute - criticalMinute, 3) : 0));
    }
}
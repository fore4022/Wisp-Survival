using UnityEngine;
/// <summary>
/// 시간에 따른 난이도를 조절하는 시스템
/// </summary>
public class DifficultyScaler
{
    private const float IncreaseRate = 0.085f;
    private const int CriticalMinute = 8;

    private float _minute;

    public float SpawnDelay { get { return Mathf.Lerp(1, 0.33f, (Managers.Game.inGameTimer.GetSeconds / 60f)) * Managers.Game.stageInformation.SpawnDelay / GetDifficultyScale(); } }
    public float IncreaseStat { get { return Managers.Game.stageInformation.StatScale * GetDifficultyScale(); } }
    private float GetDifficultyScale()
    {
        _minute = Managers.Game.inGameTimer.GetMinutes;
        
        return (1 + IncreaseRate * (Managers.Game.stageInformation.Difficulty - 1)) * (1 + (IncreaseRate * (1 + ((Managers.Game.stageInformation.Difficulty - 1) / 10))) * _minute + (_minute > CriticalMinute ? 0.001f * Mathf.Pow(_minute - CriticalMinute, 3) : 0));
    }
}
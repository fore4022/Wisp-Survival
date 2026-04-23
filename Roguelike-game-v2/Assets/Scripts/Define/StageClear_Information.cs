/// <summary>
/// 스테이지 이름과 스테이지 상태를 포함한 타입
/// </summary>

[System.Serializable]
public class StageClear_Information
{
    public StageState state;

    public string name;

    public StageClear_Information(string name, StageState state)
    {
        this.name = name;
        this.state = state;
    }
}
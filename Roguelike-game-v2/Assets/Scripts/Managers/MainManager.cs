public class MainManager
{
    public StageDatas stageDatas = new();

    public StageSO GetCurrentStageSO(int sign = 0)
    {
        return Managers.Main.stageDatas.GetSO(Managers.Data.user.StageName, sign);
    }
}
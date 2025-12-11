using System.Collections.Generic;
using System.Linq;
/// <summary>
/// <para>
/// 모든 스테이지를 담는 타입
/// </para>
/// 이어지는 스테이지를 반환
/// </summary>
[System.Serializable]
public class StageDatas
{
    private StageList_SO so;

    public StageList_SO SO { set { so = value; } }
    public List<Stage_SO> StageList { get { return so.StageList; } }
    public Stage_SO GetSO(string stageName, int sign)
    {
        int index = 0;

        for(int i = 0; i < so.StageList.Count(); i++)
        {
            if(so.StageList[i].StagePath == stageName)
            {
                index = i + sign;

                break;
            }
        }

        if(index == so.StageList.Count())
        {
            index = 0;
        }
        else if(index == -1)
        {
            index = so.StageList.Count() - 1;
        }

        Managers.Data.user.StageName = so.StageList[index].StagePath;

        return so.StageList[index];
    }
}
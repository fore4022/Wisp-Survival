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
    private StageList_SO _so;

    public StageList_SO SO { set { _so = value; } }
    public List<Stage_SO> StageList { get { return _so.StageList; } }
    public Stage_SO GetSO(string stageName, int sign)
    {
        int index = 0;

        for(int i = 0; i < _so.StageList.Count(); i++)
        {
            if(_so.StageList[i].StagePath == stageName)
            {
                index = i + sign;

                break;
            }
        }

        if(index == _so.StageList.Count())
        {
            index = 0;
        }
        else if(index == -1)
        {
            index = _so.StageList.Count() - 1;
        }

        Managers.Data.user.StageName = _so.StageList[index].StagePath;

        return _so.StageList[index];
    }
}
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>
/// 유저 정보
/// </para>
/// 스테이지 클리어 정보, 설정, 능력치, 마지막으로 플레이한 스테이지, 튜토리얼 플레이 여부를 저장
/// 데이터 관리의 편의성과 일관성을 위한 프로퍼티 및 메서드 구현
/// </summary>
public class UserData
{
    [SerializeField] private List<StageClear_Information> _stageClearInfos = new();
    [SerializeField] private Setting_Information _setting = new();
    [SerializeField] private PlayerStat _stat = new();

    [SerializeField] private string _currentStageName = "Prairie";
    [SerializeField] private int _level = 1;
    [SerializeField] private int _exp = 0;
    [SerializeField] private int _statPoint = 1;
    [SerializeField] private bool _tutorial = false;
    
    public List<StageClear_Information> StageClearInfo { get { return _stageClearInfos; } set { _stageClearInfos = value; } }
    public PlayerStat Stat { get { return _stat; } }
    public string StageName
    {
        get { return _currentStageName; }
        set 
        {
            _currentStageName = value;

            if(GetStageState() != StageState.Locked)
            {
                Managers.Data.Save();
            }
        }
    }
    public int Level 
    {
        get { return _level; }
        set
        { 
            _level = value;
            
            Managers.Data.Save();
        } 
    }
    public int Exp
    {
        get { return _exp; } 
        set
        {
            _exp = value;

            Managers.Data.Save();
        }
    }
    public int StatPoint
    {
        get { return _statPoint; }
        set
        {
            _statPoint = value;

            Managers.Data.Save();
        }
    }
    public bool Tutorial { get { return _tutorial; } set { _tutorial = value; } }
    public bool BGM { get { return _setting.BGM; } }
    public bool FX { get { return _setting.FX; } }
    // 현재 스테이지 상태(잠김, 해제, 클리어)
    public StageState GetStageState()
    {
        return _stageClearInfos.Find(info => info.name == _currentStageName).state;
    }
    // BGM 토글
    public bool SetBGM()
    {
        _setting.BGM = !_setting.BGM;

        Managers.Data.Save();

        return _setting.BGM;
    }
    // FX 토글
    public bool SetFX()
    {
        _setting.FX = !_setting.FX;

        Managers.Data.Save();

        return _setting.FX;
    }
    // 스테이지 상태를 클리어로 변경, 다음 스테이지가 존재하는 경우에 해당 스테이지의 상태를 해제로 변경
    public void Clear(string stageName)
    {
        StageClear_Information info = _stageClearInfos.Find(o => o.name == stageName);

        if(info != null)
        {
            int index = _stageClearInfos.IndexOf(info) + 1;

            if(index < _stageClearInfos.Count)
            {
                if(_stageClearInfos[index].state == StageState.Locked)
                {
                    _stageClearInfos[index].state = StageState.Unlocked;
                }
            }

            info.state = StageState.Cleared;

            Managers.Data.Save();
        }
    }
}
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// <para>
/// 유저의 데이터를 생성, 수정, 저장, 불러오기 기능 제공
/// </para>
/// 데이터는 JSON 형식으로 디바이스 환경의 저장 공간에 위치
/// </summary>
public class Data_Manager
{
    public UserData user = null;

    private UserExpTable_SO _userExpTable;

    private const string UserExpTablePath = "UserExpTable";

    private string _filePath = "";
    private bool _isSaving = false;
    
    public UserExpTable_SO UserExpTable { get { return _userExpTable; } }
    // 유저 정보와 경험치 표를 불러오며, 유저 정보가 없을 경우 생성
    public async Task Load()
    {
        _userExpTable = await AddressableHelper.LoadingToPath<UserExpTable_SO>(UserExpTablePath);

        _filePath = Path.Combine(Application.persistentDataPath, "UserData.Json");

        if(!File.Exists(_filePath))
        {
            Save();

            return;
        }

        user = JsonUtility.FromJson<UserData>(await File.ReadAllTextAsync(_filePath));
    }
    // 정보가 없을 경우 기본 상태로 저장
    public async void Save()
    {
        if(!_isSaving)
        {
            _isSaving = true;
        }

        if(user == null)
        {
            user = new();

            foreach(Stage_SO so in Managers.Main.stageDatas.StageList)
            {
                if(Managers.Data.user.StageClearInfo.Find(info => info.name == so.StagePath) == null)
                {
                    if(user.StageClearInfo.Count == 0)
                    {
                        user.StageClearInfo.Add(new(so.StagePath, StageState.Unlocked));
                    }
                    else
                    {
                        user.StageClearInfo.Add(new(so.StagePath, StageState.Unlocked)); // Locked
                    }
                }
            }
        }

        using (FileStream stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        using (StreamWriter writer = new StreamWriter(stream))
        {
            await writer.WriteAsync(JsonUtility.ToJson(user));
        }

        _isSaving = false;
    }
}
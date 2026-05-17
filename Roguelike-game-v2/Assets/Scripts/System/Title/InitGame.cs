using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 유저 정보 불러오기 및, 소리 설정 적용
/// </summary>
public class InitGame : MonoBehaviour
{
    [SerializeField] private EnterMainScene _enterMainScene;
    [SerializeField] private AudioSource _audioSource;

    private const string StageDataPath = "StageDatas";

    private Task _dataLoading;

    private void Start()
    {
        StartCoroutine(Initializing());
    }
    private async Task LoadStageDatas()
    {
        Managers.Main.stageDatas.SO = await AddressableHelper.LoadingToPath<StageListSO>(StageDataPath, false);
    }
    private IEnumerator Initializing()
    {
        Task loadStageDatas = LoadStageDatas();

        yield return new WaitUntil(() => loadStageDatas.IsCompleted);
        
        _dataLoading = Managers.Data.Load();

        yield return new WaitUntil(() => Managers.UI.IsInitalized());
        
        yield return new WaitUntil(() => Managers.Audio.Mixer != null);
        
        StartCoroutine(UserDataLoading());
    }
    private IEnumerator UserDataLoading()
    {
        yield return new WaitUntil(() => _dataLoading.IsCompleted);

        Managers.Audio.Init();
        Managers.Audio.InitializedAudio();
        Managers.UI.Get<StartMessage_UI>().SetState();
        _audioSource.Play();

        _enterMainScene.isLoad = true;
    }
}
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 유저 정보 불러오기 및, 소리 설정 적용
/// </summary>
public class InitGame : MonoBehaviour
{
    [SerializeField] private EnterMainScene enterMainScene;
    [SerializeField] private AudioSource audioSource;

    private const string _stageDataPath = "StageDatas";

    private Task dataLoading;

    private void Start()
    {
        StartCoroutine(Initializing());
    }
    private async Task LoadStageDatas()
    {
        Managers.Main.stageDatas.SO = await AddressableHelper.LoadingToPath<StageList_SO>(_stageDataPath, false);
    }
    private IEnumerator Initializing()
    {
        Task loadStageDatas = LoadStageDatas();

        yield return new WaitUntil(() => loadStageDatas.IsCompleted);

        dataLoading = Managers.Data.Load();

        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        yield return new WaitUntil(() => Managers.Audio.Mixer != null);

        StartCoroutine(UserDataLoading());
    }
    private IEnumerator UserDataLoading()
    {
        yield return new WaitUntil(() => dataLoading.IsCompleted);

        Managers.Audio.Init();
        Managers.Audio.InitializedAudio();
        Managers.UI.Get<StartMessage_UI>().SetState();
        audioSource.Play();

        enterMainScene.isLoad = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 게임 정보 불러오기 및 시스템 초기화
/// </summary>
public class GameSetter
{
    private PoolingObject_Initializer _poolingObjectInitializer = new();

    private List<GameObject> _skillList = new();
    private List<GameObject> _monsterList;
    private GameObject _damageText;
    private GameObject _stage;

    private const string UserLevelPath = "_Level";
    private const int DefaultMonsterCount = 325;
    private const int DefaultSkillCount = 40;

    private Coroutine _coroutine = null;

    private async Task LoadSkillList()
    {
        UserLevel_SO userLevel;
        SkillInformation_SO so;
        GameObject skill;

        for(int i = 1; i <= Managers.Data.user.Level; i++)
        {
            userLevel = await AddressableHelper.LoadingToPath<UserLevel_SO>($"{i}{UserLevelPath}");

            foreach(string path in userLevel.PathList)
            {
                so = await AddressableHelper.LoadingToPath<SkillInformation_SO>(path);
                skill = await AddressableHelper.LoadingToPath<GameObject>(so.Info.type);

                _skillList.Add(skill);
                Managers.Game.inGameData_Manage.skill.SetDictionaryItem(so);
            }
        }
    }
    private async Task LoadDamageText()
    {
        _damageText = await AddressableHelper.LoadingToPath<GameObject>(DamageLog_Manage.PrefabName);
    }
    private async Task LoadStage()
    {
        _stage = await AddressableHelper.LoadingToPath<GameObject>(Managers.Main.GetCurrentStageSO().StagePath);
    }
    public IEnumerator Initializing()
    {
        Time.timeScale = 0;

        Managers.Scene.LoadScene(SceneNames.InGame);

        yield return new WaitForEndOfFrame();

        CoroutineHelper.Start(Setting());
    }
    private IEnumerator Setting()
    {
        Time.timeScale = 0;

        _coroutine = CoroutineHelper.Start(DataLoading());

        yield return new WaitUntil(() => _coroutine == null);

        _coroutine = CoroutineHelper.Start(InstantiateCreating());

        yield return new WaitUntil(() => _coroutine == null);

        Managers.Audio.InitializedAudio();

        yield return new WaitUntil(() => Managers.Game.inGameData_Manage.player.levelUpdate != null);

        yield return new WaitUntil(() => Managers.Game.player != null);

        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        Managers.Scene.LoadComplete();

        yield return new WaitUntil(() => Managers.UI.Get<LoadingOverlay_UI>() == null);

        Managers.Game.Start();
    }
    private IEnumerator DataLoading()
    {
        _monsterList = Managers.Game.stageInformation.SpawnMonsterList.Monsters;

        Task loadStage = LoadStage();
        Task loadSkill = LoadSkillList();
        Task loadDamageText = LoadDamageText();

        yield return new WaitUntil(() => loadStage.IsCompleted && loadSkill.IsCompleted && loadDamageText.IsCompleted);

        Managers.Game.monsterSpawner.monsterList = _monsterList;
        Managers.Game.inGameData_Manage.player.MaxLevel = _skillList.Count;
        _coroutine = null;
    }
    private IEnumerator InstantiateCreating()
    {
        Object.Instantiate(_stage);

        Managers.Game.objectPool.Create(_monsterList, DefaultMonsterCount);
        Managers.Game.objectPool.Create(_skillList, DefaultSkillCount);
        Managers.Game.objectPool.Create(_damageText);

        int typeCount = _monsterList.Count + _skillList.Count;

        yield return new WaitUntil(() => typeCount + 1 <= Managers.Game.objectPool.PoolingObjectsCount);

        _poolingObjectInitializer.Start(_monsterList, _skillList);
        Managers.Game.damageLog_Manage.Set();

        yield return new WaitUntil(() => typeCount <= Managers.Game.so_Manage.ScriptableObjectsCount);

        yield return new WaitUntil(() => Managers.Game.damageLog_Manage.isSet);

        yield return new WaitUntil(() => _poolingObjectInitializer.Init);

        _coroutine = null;
    }
}
using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// 서브 매니저, 게임 시스템 접근 제공 및 게임의 흐름과 상태를 제어
/// </summary>
public class Game_Manager
{
    public SkillCaster_Manage skillCaster_Manage;
    public InGameData_Manage inGameData_Manage;
    public DamageLog_Manage damageLog_Manage;
    public ScriptableObject_Manage so_Manage;
    public DifficultyScaler difficultyScaler;
    public MonsterSpawner monsterSpawner;
    public GameEffect effect;
    public InGameTimer inGameTimer;
    public ObjectPool objectPool;
    public GameSetter gameSetter;
    public Player player;
    public StageInformation_SO stageInformation = null;

    public Action start;
    public Action restart;
    public Action over;

    private Coroutine reSetting = null;
    private int userExp = 0;
    private bool isPlaying = false;
    private bool gameOver = false;
    private bool stageClear = false;

    public int UserExp { get { return userExp; } set { userExp = value; } }
    public bool Playing { get { return isPlaying; } set { isPlaying = value; } }
    public bool GameOver { get { return gameOver; } }
    public bool IsStageClear { get { return stageClear; } }
    // 서브 매니저, 게임 시스템 초기화
    private void Init()
    {
        skillCaster_Manage = new();
        inGameData_Manage = new();
        damageLog_Manage = new();
        so_Manage = new();
        difficultyScaler = new();
        monsterSpawner = new();
        effect = new();
        inGameTimer = new();
        objectPool = new();
        gameSetter = new();

        SetEvent();
    }
    // 이벤트 등록
    private void SetEvent()
    {
        // start
        start += inGameTimer.StartTimer;
        start += monsterSpawner.StartSpawn;
        start += inGameData_Manage.player.SetLevel;
        // restart
        restart += skillCaster_Manage.StopAllCaster;
        restart += inGameData_Manage.skill.Reset;
        restart += skillCaster_Manage.Reset;
    }
    // 게임 정보 불러오기 및 초기화
    public void InitGame()
    {
        stageInformation = Managers.Main.GetCurrentStageSO().information;
        isPlaying = false;

        Init();
        CoroutineHelper.Start(gameSetter.Initializing(), CoroutineType.Manage);
    }
    // 이벤트 호출 및 UI 활성화
    public void Start()
    {
        isPlaying = true;
        gameOver = false;
        stageClear = false;

        start.Invoke();

        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
    }
    // 이벤트 호출, 게임 상태 및 정보 재설정
    public void ReStart()
    {
        isPlaying = false;
        gameOver = false;

        CoroutineHelper.Start(ReStarting(), CoroutineType.Manage);
    }
    // 이벤트 호출, 종료 효과 재생, 결과 표시
    public void Over()
    {
        Managers.Data.user.Exp += userExp;
        isPlaying = false;

        if(!player.Death)
        {
            effect.StageClear();
        }
        else
        {
            gameOver = true;
            effect.StageFailed();
        }

        over?.Invoke();

        Input_Manage.DisableInputAction<TouchControls>();
        Managers.UI.Hide<HpSlider_UI>();
    }
    // 서브 매니저 실행 중단, 서브 매니저 해제
    public void Clear()
    {
        start = null;
        restart = null;
        over = null;

        skillCaster_Manage.StopAllCaster();
        inGameTimer.StopTimer();
        monsterSpawner.StopSpawn();
        objectPool.StopAllActions();
        damageLog_Manage.Clear();
        AddressableHelper.ResourcesRelease();

        skillCaster_Manage = null;
        inGameData_Manage = null;
        damageLog_Manage = null;
        so_Manage = null;
        difficultyScaler = null;
        effect = null;
        inGameTimer = null;
        monsterSpawner = null;
        objectPool = null;
        gameSetter = null;
        stageInformation = null;
        userExp = 0;
    }
    // 스테이지 클리어 여부 확인
    public void IsStageCleared(int minutes)
    {
        if(minutes >= stageInformation.RequiredTime)
        {
            stageClear = true;
            isPlaying = false;
            Managers.Game.inGameTimer.minuteUpdate -= IsStageCleared;

            Over();
        }
    }
    // 게임 재설정 대기 이후 재시작
    private IEnumerator ReStarting()
    {
        reSetting = CoroutineHelper.Start(ReSetting(), CoroutineType.Manage);

        yield return null;

        yield return new WaitUntil(() => reSetting == null);

        Time.timeScale = 1;
        userExp = 0;
        stageClear = false;

        monsterSpawner.ReStart();
        inGameData_Manage.player.SetLevel();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Show<LevelUp_UI>();
        Managers.UI.Show<HpSlider_UI>();
    }
    // 서브 매니저, UI, 정보 초기화
    private IEnumerator ReSetting()
    {
        Managers.UI.Show<LoadingOverlay_UI>();

        yield return new WaitUntil(() => Managers.UI.Get<LoadingOverlay_UI>() != null);

        yield return new WaitUntil(() => !Managers.UI.Get<LoadingOverlay_UI>().IsFadeIn);

        Camera.main.orthographicSize = CameraSizes.inGame * Camera_SizeScale.orthographicSizeScale;
        Managers.Game.inGameData_Manage.player.Experience = 0;
        isPlaying = true;

        restart.Invoke();

        objectPool.ReSetting();
        player.Reset();
        Managers.UI.Hide<GameOver_UI>();
        Managers.UI.Get<LoadingOverlay_UI>().FadeOut();

        inGameTimer.ReStart();
        Input_Manage.EnableInputAction<TouchControls>();

        reSetting = null;
    }
}
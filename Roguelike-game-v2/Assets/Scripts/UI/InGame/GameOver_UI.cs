using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class GameOver_UI : UserInterface
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clear;
    [SerializeField] private AudioClip _failed;
    [SerializeField] private AudioClip _buttonClickSfx;

    private List<TextMeshProUGUI> _tmpList;
    private List<Image> _imgList;
    private TextMeshProUGUI _result;

    private const string Arrow = "->";
    private const float Delay = 0.225f;
    private readonly WaitForSecondsRealtime _waitRealSec = new(Delay);

    public override void SetUserInterface()
    {
        _audioSource = GetComponent<AudioSource>();
        _tmpList = transform.GetChild(1).GetComponentsInChild<TextMeshProUGUI>(true);
        _imgList = transform.GetChild(1).GetComponentsInChild<Image>();
        _result = transform.GetComponentInChildren<TextMeshProUGUI>(transform);

        Managers.UI.Hide<GameOver_UI>();
    }
    protected override void Enable()
    {
        string result;

        Time.timeScale = 0;

        Managers.UI.Hide<LevelUp_UI>();
        Managers.UI.Hide<HeadUpDisplay_UI>();

        if(Managers.Game.IsStageClear || Managers.Game.inGameTimer.GetHours > 0)
        {
            _audioSource.clip = _clear;
            result = "Stage\nClear";

            Managers.Data.user.Clear(Managers.Data.user.StageName);
        }
        else
        {
            _audioSource.clip = _failed;
            result = "Stage\nFailed";
        }

        _audioSource.Play();
        StartCoroutine(Typing.TypeEffecting(_result, result, true));
        StartCoroutine(ResultSequence());
    }
    private void OnDisable()
    {
        foreach(Image img in _imgList)
        {
            UIElementUtility.SetImageAlpha(img, 0);
            img.gameObject.SetActive(false);
        }

        foreach(TextMeshProUGUI tmp in _tmpList)
        {
            tmp.text = "";
        }

        _tmpList[0].gameObject.SetActive(true);
        _tmpList[1].gameObject.SetActive(false);
        _tmpList[2].gameObject.SetActive(false);

        _tmpList[0].rectTransform.anchoredPosition = new(-175, 195);
        _tmpList[1].rectTransform.anchoredPosition = new(175, 195);
        _tmpList[2].rectTransform.anchoredPosition = new(175, 195);

        _result.text = "";
    }
    public void Play()
    {
        Time.timeScale = 1;
        Managers.Game.Playing = true;

        _audioSource.Play();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Hide<GameOver_UI>();
        Managers.Game.effect.ContinuePlay();
    }
    public void ReStart()
    {
        _audioSource.Play();
        Managers.Game.ReStart();
    }
    public void GoMain()
    {
        _audioSource.Play();
        Managers.Game.Clear();
        Managers.Scene.LoadScene(SceneNames.Main, false);
    }
    private IEnumerator ResultSequence()
    {
        string required = $"목표 시간\n\n{(Managers.Game.stageInformation.RequiredTime / 60):D2} : {Managers.Game.stageInformation.RequiredTime:D2} : 00";
        string survival = $"생존 시간\n\n{Managers.Game.inGameTimer.GetHours:D2} : {Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
        string gainExp = $"Experience\n\n+ {Managers.Game.UserExp:N0} EXP";

        yield return _waitRealSec;

        yield return Typing.EffectAndGetWaiting(_tmpList[0], required, Delay);

        _tmpList[0].transform.DOMove(new(-175, 195), Delay);
        _tmpList[1].gameObject.SetActive(true);

        yield return Typing.EffectAndGetWaiting(_tmpList[1], Arrow);

        _tmpList[1].transform.DOMove(new(-175, 195), Delay);

        StartCoroutine(Typing.EraseEffecting(_tmpList[0], Delay));

        yield return new WaitForSecondsRealtime(Delay);

        _tmpList[0].gameObject.SetActive(false);
        _tmpList[2].gameObject.SetActive(true);

        yield return Typing.EffectAndGetWaiting(_tmpList[2], survival);

        _tmpList[2].transform.DOMove(new(0, 195), Delay);

        StartCoroutine(Typing.EraseEffecting(_tmpList[1], Delay));

        yield return new WaitForSecondsRealtime(Delay);

        _tmpList[1].gameObject.SetActive(false);

        yield return Typing.EffectAndGetWaiting(_tmpList[3], gainExp, Delay);

        yield return _waitRealSec;

        _audioSource.clip = _buttonClickSfx;

        if(Managers.Game.IsStageClear && !Managers.Game.GameOver)
        {
            _imgList[0].gameObject.SetActive(true);
            _imgList[1].gameObject.SetActive(false);
        }
        else
        {
            _imgList[0].gameObject.SetActive(false);
            _imgList[1].gameObject.SetActive(true);
        }

        _imgList[2].gameObject.SetActive(true);

        foreach(Image img in _imgList)
        {
            UIElementUtility.SetImageAlpha(img, 255, Delay, true);
        }

        Time.timeScale = 0;
    }
}
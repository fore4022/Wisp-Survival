using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting_UI : UserInterface
{
    [SerializeField] private List<Sprite> _bgmSprite;
    [SerializeField] private List<Sprite> _fxSprite;
    [SerializeField] private Image _Bgm;
    [SerializeField] private Image _Fx;
    [SerializeField] private AudioClip onToggleSound;
    [SerializeField] private AudioClip offToggleSound;
    [SerializeField] private AudioClip confirmSound;

    private AudioSource audioSource;

    private const string _sceneName = "InGame";

    private bool _isInGame;

    public override void SetUserInterface()
    {
        audioSource = transform.parent.GetComponent<AudioSource>();
        _isInGame = Managers.Scene.CurrentSceneName == _sceneName ? true : false;

        Managers.UI.Hide<Setting_UI>();
    }
    protected override void Enable()
    {
        if(_isInGame)
        {
            Managers.UI.Get<PauseMenu_UI>().HideIcons();
        }

        BgmUpdate();
        SfxUpdate();
    }
    public void ToggleBGM()
    {
        Managers.Audio.SetGroup(SoundTypes.BGM);
        BgmUpdate();
        audioSource.Play();
    }
    public void ToggleSFX()
    {
        Managers.Audio.SetGroup(SoundTypes.FX);
        SfxUpdate();
        audioSource.Play();
    }
    private void BgmUpdate()
    {
        if(Managers.Data.user.BGM)
        {
            audioSource.clip = onToggleSound;
            _Bgm.sprite = _bgmSprite[0];
        }
        else
        {
            audioSource.clip = offToggleSound;
            _Bgm.sprite = _bgmSprite[1];
        }
    }
    private void SfxUpdate()
    {
        if(Managers.Data.user.FX)
        {
            audioSource.clip = onToggleSound;
            _Fx.sprite = _fxSprite[0];
        }
        else
        {
            audioSource.clip = offToggleSound;
            _Fx.sprite = _fxSprite[1];
        }
    }
    public void Confirm()
    {
        if(_isInGame)
        {
            Managers.UI.Get<PauseMenu_UI>().ShowIcons();
        }

        audioSource.clip = confirmSound;

        audioSource.Play();
        Managers.UI.Hide<Setting_UI>();
    }
}
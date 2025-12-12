using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting_UI : UserInterface
{
    [SerializeField] private List<Sprite> _bgmSprite;
    [SerializeField] private List<Sprite> _fxSprite;
    [SerializeField] private Image _bgm;
    [SerializeField] private Image _fx;
    [SerializeField] private AudioClip _onToggleSound;
    [SerializeField] private AudioClip _offToggleSound;
    [SerializeField] private AudioClip _confirmSound;

    private AudioSource _audioSource;

    private const string SceneName = "InGame";

    private bool _isInGame;

    public override void SetUserInterface()
    {
        _audioSource = transform.parent.GetComponent<AudioSource>();
        _isInGame = Managers.Scene.CurrentSceneName == SceneName ? true : false;

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
        _audioSource.Play();
    }
    public void ToggleSFX()
    {
        Managers.Audio.SetGroup(SoundTypes.FX);
        SfxUpdate();
        _audioSource.Play();
    }
    private void BgmUpdate()
    {
        if(Managers.Data.user.BGM)
        {
            _audioSource.clip = _onToggleSound;
            _bgm.sprite = _bgmSprite[0];
        }
        else
        {
            _audioSource.clip = _offToggleSound;
            _bgm.sprite = _bgmSprite[1];
        }
    }
    private void SfxUpdate()
    {
        if(Managers.Data.user.FX)
        {
            _audioSource.clip = _onToggleSound;
            _fx.sprite = _fxSprite[0];
        }
        else
        {
            _audioSource.clip = _offToggleSound;
            _fx.sprite = _fxSprite[1];
        }
    }
    public void Confirm()
    {
        if(_isInGame)
        {
            Managers.UI.Get<PauseMenu_UI>().ShowIcons();
        }

        _audioSource.clip = _confirmSound;

        _audioSource.Play();
        Managers.UI.Hide<Setting_UI>();
    }
}
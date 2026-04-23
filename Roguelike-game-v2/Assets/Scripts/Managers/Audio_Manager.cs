using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager
{
    private AudioMixer _audioMixer;

    private const float MaxValue_BGM = -5;
    private const float MaxValue_FX = 2.5f;
    private const float MinValue = -80;

    public AudioMixer Mixer { get { return _audioMixer; } set { _audioMixer = value; } }

    public void Init()
    {
        SetGroup(SoundTypes.FX, Managers.Data.user.FX);
        SetGroup(SoundTypes.BGM, Managers.Data.user.BGM);
    }

    public void InitializedAudio()
    {
        GameObject[] objs = Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (GameObject obj in objs)
        {
            if(obj.TryGetComponent(out AudioSource audio))
            {
                Registration(audio);
            }
        }
    }

    public void Registration(AudioSource source)
    {
        if(_audioMixer == null)
        {
            CoroutineHelper.Start(WaitForAudioMixer(source), CoroutineType.Manage);
        }
        else
        {
            SetOutputMixerGroup(source);
        }
    }

    public void SetGroup(SoundTypes type)
    {
        if(type == SoundTypes.FX)
        {
            SetGroup(type, Managers.Data.user.SetFX());
        }
        else if(type == SoundTypes.BGM)
        {
            SetGroup(type, Managers.Data.user.SetBGM());
        }
    }

    private void SetGroup(SoundTypes type, bool isActive)
    {
        float value = default;

        if(isActive)
        {
            if(type == SoundTypes.FX)
            {
                value = MaxValue_FX;
            }
            else if(type == SoundTypes.BGM)
            {
                value = MaxValue_BGM;
            }
        }
        else
        {
            value = MinValue;
        }

        _audioMixer.SetFloat(type.ToString(), value);
    }

    public void SetOutputMixerGroup(AudioSource audio)
    {
        if(audio.outputAudioMixerGroup == null)
        {
            audio.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/FX")[0];
        }
        else
        {
            if(audio.outputAudioMixerGroup.name == "FX")
            {
                audio.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/FX")[0];
            }
            else if(audio.outputAudioMixerGroup.name == "BGM")
            {
                audio.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master/BGM")[0];
            }
        }

        if(audio.playOnAwake)
        {
            if(audio.gameObject.activeSelf)
            {
                audio.Play();
            }
        }
    }

    private IEnumerator WaitForAudioMixer(AudioSource source)
    {
        source.Stop();

        yield return new WaitUntil(() => _audioMixer != null);

        SetOutputMixerGroup(source);
    }
}
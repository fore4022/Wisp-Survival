using TMPro;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class StatElementUpgrade_UI : UserInterface
{
    public TextMeshProUGUI tmp;
    public AudioSource audioSource;
    public GameObject inc;
    public GameObject dec;

    private FileReference _file;

    private const string Log1 = "You are lacking stat points.";
    private const string Log2 = "Stat points cannot be used.";

    private float _value;

    public override void SetUserInterface()
    {
        tmp = transform.GetComponentInChild<TextMeshProUGUI>(true);
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }
    public void Set(FileReference file)
    {
        this._file = file;
        _value = (float)file.GetValue();

        ChangeAmount(0);
    }
    public void ChangeAmount(int sign)
    {
        if(!LacksStatPoints(sign) || !CanUseStatPoints(sign))
        {
            AudioPlay(0);

            return;
        }

        if(_value == 0 && sign == 1)
        {
            dec.SetActive(true);
        }
        else if(_value == PlayerStat_Manage.MaxLevel && sign == -1)
        {
            inc.SetActive(true);
        }

        if(sign != 0)
        {
            AudioPlay(sign);
        }

        _value += sign;
        tmp.text = $"+ {_value}";
        Managers.Data.user.StatPoint -= sign;

        _file.SetValue(_value);
        Managers.UI.Get<StatUpgrade_UI>().TextUpdate();

        if(_value == 0)
        {
            dec.SetActive(false);
        }
        else if(_value == PlayerStat_Manage.MaxLevel)
        {
            inc.SetActive(false);
        }
    }
    private bool LacksStatPoints(int sign)
    {
        if(Managers.Data.user.StatPoint == 0)
        {
            if(sign == 1 || (sign == -1 && _value == 0))
            {
                Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(Log1);

                return false;
            }
        }

        return true;
    }
    private bool CanUseStatPoints(int sign)
    {
        if((_value == 0 && sign == -1) || (_value == PlayerStat_Manage.MaxLevel && sign == 1))
        {
            Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(Log2);

            return false;
        }

        return true;
    }
    private void AudioPlay(int sign)
    {
        if(sign == 0)
        {
            audioSource.clip = Managers.UI.Get<StatUpgrade_UI>().ActionUnavailableSound;
        }
        else if(sign == 1)
        {
            audioSource.clip = Managers.UI.Get<StatUpgrade_UI>().IncreaseSound;
        }
        else if(sign == -1)
        {
            audioSource.clip = Managers.UI.Get<StatUpgrade_UI>().DecreaseSound;
        }

        audioSource.Play();
    }
}
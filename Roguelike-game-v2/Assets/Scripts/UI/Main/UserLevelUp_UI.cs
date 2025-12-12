using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class UserLevelUp_UI : UserInterface, IPointerClickHandler
{
    [SerializeField] private GameObject[] _particles;
    [SerializeField] private TextMeshProUGUI _log;
    [SerializeField] private TextMeshProUGUI _prompt;

    private const float DelaySec = 0.8f;
    private const float Duration = 1.5f;
    private const int MaxCount = 6;

    private WaitForSeconds _delay = new(DelaySec);
    private bool _allowClose = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!_allowClose)
        {
            return;
        }

        for(int i = 0; i < MaxCount; i++)
        {
            _particles[i].SetActive(false);
        }

        Managers.UI.Get<StatUpgrade_UI>().TextUpdate();
        gameObject.SetActive(false);
    }
    public override void SetUserInterface()
    {
        gameObject.SetActive(false);
    }
    public void OnValidate()
    {
        ArrayUtil.ResizeArray(ref _particles, MaxCount);
    }
    public void PlayEffect(int levelUpCount)
    {
        StartCoroutine(LevelTextEffecting(levelUpCount));
        StartCoroutine(ParticleEffecting());
    }
    private IEnumerator LevelTextEffecting(int levelUpCount)
    {
        string str = $"Lv. {Managers.Data.user.Level - levelUpCount}";
        int length = str.Length;

        _log.text = str;

        yield return _delay;

        str = $"Lv. {Managers.Data.user.Level}";

        StartCoroutine(Typing.TypeEffecting(_log, " -> " + str));

        yield return _delay;

        _prompt.gameObject.SetActive(true);
        StartCoroutine(Typing.EraseEffecting(_log, length));
        StartCoroutine(UIElementUtility.BlinkText(_prompt, Duration, false, 65));

        _allowClose = true;
    }
    private IEnumerator ParticleEffecting()
    {
        int[] indexs = Default_Calculate.GetRandomValues(MaxCount);

        while(true)
        {
            for(int i = 0; i < MaxCount; i++)
            {
                if(!_particles[i].activeSelf)
                {
                    _particles[i].SetActive(true);
                    _particles[i].transform.position = Default_Calculate.GetRandomVector();

                    yield return _delay;
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
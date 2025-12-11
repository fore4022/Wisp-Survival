using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class UserLevelUp_UI : UserInterface, IPointerClickHandler
{
    [SerializeField] private GameObject[] particles;
    [SerializeField] private TextMeshProUGUI log;
    [SerializeField] private TextMeshProUGUI prompt;

    private const float delaySec = 0.8f;
    private const float duration = 1.5f;
    private const int maxCount = 6;

    private WaitForSeconds delay = new(delaySec);
    private bool allowClose = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!allowClose)
        {
            return;
        }

        for(int i = 0; i < maxCount; i++)
        {
            particles[i].SetActive(false);
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
        ArrayUtil.ResizeArray(ref particles, maxCount);
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

        log.text = str;

        yield return delay;

        str = $"Lv. {Managers.Data.user.Level}";

        StartCoroutine(Typing.TypeEffecting(log, " -> " + str));

        yield return delay;

        prompt.gameObject.SetActive(true);
        StartCoroutine(Typing.EraseEffecting(log, length));
        StartCoroutine(UIElementUtility.BlinkText(prompt, duration, false, 65));

        allowClose = true;
    }
    private IEnumerator ParticleEffecting()
    {
        int[] indexs = Default_Calculate.GetRandomValues(maxCount);

        while(true)
        {
            for(int i = 0; i < maxCount; i++)
            {
                if(!particles[i].activeSelf)
                {
                    particles[i].SetActive(true);
                    particles[i].transform.position = Default_Calculate.GetRandomVector();

                    yield return delay;
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
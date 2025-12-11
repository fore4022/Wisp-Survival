using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
/// <summary>
/// <para>
/// UI 요소의 Button에 대한 구현이다.
/// </para>
/// UserInterface가 아닌 하나의 요소인 Button이다.
/// </summary>
public abstract class Button_Default : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;

    protected RectTransform rectTransform;
    protected Button button;

    protected void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        AddButtonEvents();
    }
    protected virtual void AddButtonEvents()
    {
        button.onClick.AddListener(() =>
        {
            PointerClick();
            audioSource.Play();
        });
    }
    protected abstract void PointerClick();
}
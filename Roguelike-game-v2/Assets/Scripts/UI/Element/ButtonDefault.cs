using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
/// <summary>
/// <para>
/// UI 요소의 Button에 대한 구현이다.
/// </para>
/// UserInterface가 아닌 하나의 요소인 Button이다.
/// </summary>
public abstract class ButtonDefault : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioSource;

    protected RectTransform _rectTransform;
    protected Button _button;

    protected void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();
        
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }

        _audioSource.playOnAwake = false;
        _audioSource.loop = false;

        AddButtonEvents();
    }
    protected virtual void AddButtonEvents()
    {
        _button.onClick.AddListener(() =>
        {
            PointerClick();
            _audioSource.Play();
        });
    }
    protected abstract void PointerClick();
}
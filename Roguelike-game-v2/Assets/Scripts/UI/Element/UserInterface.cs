using UnityEngine;
/// <summary>
/// <para>
/// 모든 UserInterface의 기본 구현
/// </para>
/// UserInterface를 상속받은 클래스들은 UI_Manager를 통해서 접근 가능
/// </summary>
public abstract class UserInterface : MonoBehaviour
{
    protected bool _isInitalized = false;

    public bool IsInitalized { get { return _isInitalized; } }
    // 초기화 되지 않았을 경우 UI_Manager에 등록, 이후 활성화되는 경우 Enable 실행
    protected void OnEnable()
    {
        if(!_isInitalized)
        {
            Managers.UI.Add(this);
        }
        else
        {
            Enable();
        }
    }
    // UI 초기화 호출
    protected virtual void Start()
    {
        InitUI();
    }
    // 초기화 이후 UI가 활성화 시점의 동작 구현
    protected virtual void Enable() { }
    // UI 초기화, UI_Manager에 접근하도록 별도 메서드 생성
    public void InitUI()
    {
        if(!_isInitalized)
        {
            _isInitalized = true;

            SetUserInterface();
        }
    }
    // 초기화 구현
    public abstract void SetUserInterface();
}
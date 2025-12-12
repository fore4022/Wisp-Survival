using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
/// <summary>
/// <para>
/// UI 로드, 생성, 표시·숨김, 파괴를 통한 관리 기능 제공
/// </para>
/// Addressable과 코루틴을 이용해 UI를 생성하고, 초기화
/// UI 클래스 타입을 통해서 해당하는 UI에 접근
/// </summary>
public class UI_Manager
{
    private Dictionary<string, UserInterface> _uiDictionary = new();

    private Transform _root;
    
    // 생성된 UI가 위치하는 Root 반환
    private Transform Transform
    {
        get 
        {
            if(_root == null)
            {
                GameObject go = GameObject.Find("UI");

                if(go == null)
                {
                    go = new GameObject { name = "UI" };
                }

                _root = go.transform;
            }

            return _root;
        }
    }
    // 모든 UI의 초기화 여부를 확인, 초기화되지 않은 UI의 초기화를 실행
    public bool IsInitalized()
    {
        foreach(UserInterface ui in _uiDictionary.Values)
        {
            if(!ui.IsInitalized)
            {
                ui.InitUI();

                return false;
            }
        }

        return true;
    }
    // 타입에 해당하는 UI를 활성화, 해당하는 UI가 없다면 생성한 이후 활성화
    public void Show<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(_uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(true);
        }
        else
        {
            CoroutineHelper.Start(Creating(name, true), CoroutineType.Manage);
        }
    }
    // 타입에 해당하는 UI를 비활성화
    public void Hide<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(_uiDictionary.TryGetValue(name, out UserInterface ui))
        {
            ui.gameObject.SetActive(false);
        }
    }
    // 타입에 해당하는 UI가 존재하지 않을 경우 생성, 생성 이후 활성화 여부 지정 가능
    public void Create<T>(bool isActive = false) where T : UserInterface
    {
        string name = GetName<T>();

        if(!_uiDictionary.ContainsKey(name))
        {
            Creating(name, isActive);
        }
    }
    // 타입에 해당하는 UI 인스턴스를 반환
    public T Get<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(_uiDictionary.ContainsKey(name))
        {
            return _uiDictionary[name].GetComponentInChildren<T>();
        }

        return null;
    }
    // 타입에 해당하는 UI가 존재한다면 제거
    public void Destroy<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(_uiDictionary.ContainsKey(name))
        {
            Object.Destroy(_uiDictionary[name].gameObject);

            _uiDictionary.Remove(name);
        }
    }
    // UI 등록
    public void Add(UserInterface ui)
    {
        string name = ui.GetType().ToString().Replace("_UI", "");

        if(!_uiDictionary.ContainsKey(name))
        {
            _uiDictionary.Add(name, ui);
        }
    }
    // 타입에 해당하는 UI 활성화 및 인스턴스 반환
    public T ShowAndGet<T>() where T : UserInterface
    {
        string name = GetName<T>();

        if(_uiDictionary.ContainsKey(name))
        {
            _uiDictionary[name].gameObject.SetActive(true);

            return _uiDictionary[name].GetComponentInChildren<T>();
        }

        return null;
    }
    // UI 등록 초기화
    public void Clear()
    {
        _uiDictionary = new();
    }
    // 타입을 통해서 UI 딕셔너리 키 반환
    private string GetName<T>() where T : UserInterface
    {
        return typeof(T).ToString().Replace("_UI", "");
    }
    // Addressable로 UI 프리팹 불러오기
    private async Task LoadUI(string uiName)
    {
        GameObject go = await AddressableHelper.LoadingToPath<GameObject>(uiName);

        if(!_uiDictionary.ContainsKey(uiName))
        {
            _uiDictionary.Add(uiName, go.GetComponent<UserInterface>());
        }
    }
    // 입력받은 키의 UI 로드 대기 이후 생성 및 활성화 상태 설정
    private IEnumerator Creating(string uiName, bool isActive)
    {
        UserInterface ui;
        
        Task loadUI = LoadUI(uiName);

        yield return new WaitUntil(() => loadUI.IsCompleted);

        ui = _uiDictionary[uiName];

        if(ui == null)
        {
            _uiDictionary.Remove(uiName);
        }
        else
        {
            ui = _uiDictionary[uiName] = Object.Instantiate(_uiDictionary[uiName], Transform);

            ui.gameObject.SetActive(isActive);

            if(isActive)
            {
                ui.InitUI();
            }
        }
    }
}
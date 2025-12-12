using UnityEngine;
/// <summary>
/// <para>
/// 오브젝트 풀로 생성된 오브젝트를 담는 타입
/// </para>
/// Transform, Animator, SpriteRenderer, activeSelf, GetComponent, SetActive, GetType, StopAllCoroutines 사용 가능
/// </summary>
public class PoolingObject
{
    public bool isInUse = false;
    public bool isUsed = false;

    private GameObject _go;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public GameObject GameObject { get { return _go; } }
    public Transform Transform { get { return _go.transform; } }
    public Animator Animator { get { return GetType(ref _animator); } }
    public SpriteRenderer SpriteRenderer { get { return GetType(ref _spriteRenderer); } }
    public string Name { get { return _go.name; } }
    // 게임 오브젝트 활성화 여부 반환, 게임 종료 시 false 반환
    public bool activeSelf 
    {
        get 
        {
            if(_go == null || Managers.Game.GameOver)
            {
                return false;
            }
            else
            {
                return _go.activeSelf;
            }
        } 
    }
    // 게임 오브젝트에서 지정한 타입의 컴포넌트 가져와서 반환
    public T GetComponent<T>()
    {
        return _go.GetComponent<T>();
    }
    // 게임 오브젝트의 활성화 상태를 변경, 게임 종료 또는 게임 오브젝트가 존재하지 않는 경우에 실행되지 않음
    public void SetActive(bool active)
    {
        if(_go == null || Managers.Game.GameOver)
        {
            return;
        }

        isInUse = active;
        isUsed = true;

        _go.SetActive(active);
    }
    // 게임 오브젝트에서 실행되는 모든 코루틴을 종료한다.
    public void StopAllCoroutines()
    {
        _go.GetComponent<MonoBehaviour>().StopAllCoroutines();
    }
    // variable이 null일 경우 지정한 타입의 컴포넌트를 가져와 할당, 할당되어 있다면 즉시 variable 반환
    private T GetType<T>(ref T variable)
    {
        if(variable != null)
        {
            return variable;
        }

        return variable = _go.GetComponent<T>();
    }
    public PoolingObject(GameObject go)
    {
        this._go = go;
    }
}
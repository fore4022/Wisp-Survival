using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// <para>
/// 현재 Title의 Tween 효과가 재생 중이면, Tween 효과를 종료 시점으로 변경
/// </para>
/// 현재 Title의 Tween 효과가 재생되지 않고 있다면, Main Scene으로 이동
/// </summary>
public class EnterMainScene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Title_TW _titleTW;
    [HideInInspector] public bool isLoad = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isLoad)
        {
            if(!_titleTW.CanEnterMain)
            {
                return;
            }

            isLoad = false;

            Managers.UI.Get<StartMessage_UI>().SetState();
            Managers.Scene.LoadScene(SceneNames.Main, false);
        }
    }
}
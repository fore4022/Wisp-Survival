using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Tutorial_MaskImage_UI : UserInterface, IPointerEnterHandler
{
    [SerializeField] private List<Transform> _targetList;
    [SerializeField] private List<TextMeshProUGUI> _textList;

    private Image _maskImage;

    private const string StepName = "Step_";

    private Coroutine _step = null;
    private Coroutine _typing = null;
    private string _targetStr =  "";
    private int _stepIndex = 0;

    public override void SetUserInterface()
    {
        Managers.UI.Hide<Tutorial_MaskImage_UI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_step == null)
        {
            StartCoroutine($"{StepName}{_stepIndex}");

            _stepIndex++;
        }
        else
        {
            StopCoroutine(_typing);
        }
    }
    protected override void Enable()
    {
        _stepIndex = 0;

        UIElementUtility.SetImageAlpha(_maskImage, 0);

        _step = StartCoroutine(Step_0());
    }
    private IEnumerator Step_0()
    {
        // parent SetAsLastbinding

        transform.SetParent(_targetList[_stepIndex]);

        _targetStr = "아래 플레이 버튼으로\n게임을 시작할 수 있습니다.";
        _typing = StartCoroutine(Typing.TypeEffecting(_textList[0], _targetStr));

        yield return null;

        _step = null;
    }
}

//Managers.UI.Hide<Tutorial_MaskImage_UI>();
//Managers.Data.data.Tutorial = true;
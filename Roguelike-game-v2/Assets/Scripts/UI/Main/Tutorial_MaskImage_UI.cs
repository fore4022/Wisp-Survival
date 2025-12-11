using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Tutorial_MaskImage_UI : UserInterface, IPointerEnterHandler
{
    [SerializeField] private List<Transform> targetList;
    [SerializeField] private List<TextMeshProUGUI> textList;

    private Image maskImage;

    private const string stepName = "Step_";
    private const float targetAlpha = 165;

    private Coroutine step = null;
    private Coroutine typing = null;
    private string targetStr =  "";
    private int stepIndex = 0;

    public override void SetUserInterface()
    {
        Managers.UI.Hide<Tutorial_MaskImage_UI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(step == null)
        {
            StartCoroutine($"{stepName}{stepIndex}");

            stepIndex++;
        }
        else
        {
            StopCoroutine(typing);
        }
    }
    protected override void Enable()
    {
        stepIndex = 0;

        UIElementUtility.SetImageAlpha(maskImage, 0);

        step = StartCoroutine(Step_0());
    }
    private IEnumerator Step_0()
    {
        // parent SetAsLastbinding

        transform.SetParent(targetList[stepIndex]);

        targetStr = "아래 플레이 버튼으로\n게임을 시작할 수 있습니다.";
        typing = StartCoroutine(Typing.TypeEffecting(textList[0], targetStr));

        yield return null;

        step = null;
    }
}

//Managers.UI.Hide<Tutorial_MaskImage_UI>();
//Managers.Data.data.Tutorial = true;
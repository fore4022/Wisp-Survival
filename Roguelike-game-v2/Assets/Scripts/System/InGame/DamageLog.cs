using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
public class DamageLog : MonoBehaviour
{
    private TextMeshProUGUI _log;

    private readonly Vector3 _adjustmentPosition = new(0, AdjustmentYPos, 0);
    private readonly Vector3 _defaultScale = new(DefaultScaleSize, DefaultScaleSize);
    private const float DefaultFontSize = 64;
    private const float Duration = 0.35f;
    private const float TargetScale = 0.003f;
    private const float DefaultScaleSize = 0.005f;
    private const float AdjustmentYPos = 0.075f;

    private void Awake()
    {
        _log = GetComponent<TextMeshProUGUI>();  

        _log.enabled = false;

        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(Effecting());
    }
    public void SetInformation(Vector3 position, float damage)
    {
        if(damage == 0)
        {
            gameObject.SetActive(false);
        }

        float adjustmentFontSize = DefaultFontSize;

        transform.position = position;
        _log.text = $"{(int)damage:N0}";

        for(int i = 1; i < _log.text.Length - ((_log.text.Length - 1) % 3); i++)
        {
            adjustmentFontSize /= 2;
        }

        _log.fontSize = DefaultFontSize + adjustmentFontSize;
    }
    private IEnumerator Effecting()
    {
        Vector3 targetPosition = transform.position + _adjustmentPosition;

        transform.localScale = _defaultScale;

        UIElementUtility.SetTextAlpha(_log, 255);
        UIElementUtility.SetTextAlpha(_log, 150, Duration);

        transform.DOScale(TargetScale, Duration);
        transform.DOMove(targetPosition, Duration);

        _log.enabled = true;

        yield return new WaitForSeconds(Duration);

        _log.enabled = false;

        Managers.Game.objectPool.DisableObject(gameObject, DamageLogManage.PrefabName);
    }
}
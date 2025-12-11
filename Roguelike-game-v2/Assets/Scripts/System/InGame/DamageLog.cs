using System.Collections;
using TMPro;
using UnityEngine;
public class DamageLog : MonoBehaviour
{
    private TextMeshProUGUI log;

    private readonly Vector3 adjustmentPosition = new(0, adjustmentYPos, 0);
    private readonly Vector3 defaultScale = new(defaultScaleSize, defaultScaleSize);
    private const float defaultFontSize = 36;
    private const float duration = 0.35f;
    private const float targetScale = 0.003f;
    private const float defaultScaleSize = 0.005f;
    private const float adjustmentYPos = 0.075f;

    private void Awake()
    {
        log = GetComponent<TextMeshProUGUI>();  

        log.enabled = false;

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

        float adjustmentFontSize = defaultFontSize;

        transform.position = position;
        log.text = $"{(int)damage:N0}";

        for(int i = 1; i < log.text.Length - ((log.text.Length - 1) % 3); i++)
        {
            adjustmentFontSize /= 2;
        }

        log.fontSize = defaultFontSize + adjustmentFontSize;
    }
    private IEnumerator Effecting()
    {
        Vector3 targetPosition = transform.position + adjustmentPosition;

        transform.localScale = defaultScale;

        UIElementUtility.SetTextAlpha(log, 255);
        UIElementUtility.SetTextAlpha(log, 150, duration);
        transform.SetPosition(targetPosition, duration).
            SetScale(targetScale, duration);

        log.enabled = true;

        yield return new WaitForSeconds(duration);

        log.enabled = false;

        Managers.Game.objectPool.DisableObject(gameObject, DamageLog_Manage.prefabName);
    }
}
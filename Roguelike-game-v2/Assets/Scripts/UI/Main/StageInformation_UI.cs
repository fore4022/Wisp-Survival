using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class StageInformation_UI : UserInterface
{
    [SerializeField] private List<GameObject> coverList;
    [SerializeField] private TextMeshProUGUI requiredTime;
    [SerializeField] private TextMeshProUGUI difficulty;

    private StageInformation_SO so;

    public override void SetUserInterface()
    {
        OnDisable();

        Managers.UI.Hide<StageInformation_UI>();
    }
    protected override void Enable()
    {
        foreach(GameObject go in coverList)
        {
            go.SetActive(true);
        }

        requiredTime.enabled = true;
        difficulty.enabled = true;

        Set();
    }
    public void Set()
    {
        so = Managers.Main.GetCurrentStageSO(0).information;

        requiredTime.text = $"Required Time\n {so.RequiredTime} : 00";
        difficulty.text = $"Difficulty\n{so.Difficulty}";
    }
    private void OnDisable()
    {
        foreach(GameObject go in coverList)
        {
            go.SetActive(false);
        }

        requiredTime.enabled = false;
        difficulty.enabled = false;
    }
}
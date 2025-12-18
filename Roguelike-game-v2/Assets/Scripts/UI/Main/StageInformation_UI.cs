using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class StageInformation_UI : UserInterface
{
    [SerializeField] private List<GameObject> _coverList;
    [SerializeField] private TextMeshProUGUI _requiredTime;
    [SerializeField] private TextMeshProUGUI _difficulty;

    private StageInformation_SO _so;

    public override void SetUserInterface()
    {
        OnDisable();

        Managers.UI.Hide<StageInformation_UI>();
    }
    protected override void Enable()
    {
        foreach(GameObject go in _coverList)
        {
            go.SetActive(true);
        }

        _requiredTime.enabled = true;
        _difficulty.enabled = true;

        Set();
    }
    public void Set()
    {
        _so = Managers.Main.GetCurrentStageSO(0).Information;

        _requiredTime.text = $"Required Time\n {_so.RequiredTime} : 00";
        _difficulty.text = $"Difficulty\n{_so.Difficulty}";
    }
    private void OnDisable()
    {
        foreach(GameObject go in _coverList)
        {
            go.SetActive(false);
        }

        _requiredTime.enabled = false;
        _difficulty.enabled = false;
    }
}
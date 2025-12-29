using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(PlayerStat_Manage))]
public class StatUpgrade_UI : UserInterface
{
    [SerializeField] private Transform _statElement_parent;
    [SerializeField] private AudioClip _increaseSound;
    [SerializeField] private AudioClip _decreaseSound;
    [SerializeField] private AudioClip _actionUnavailableSound;

    public List<FileReference> files;
    public TextMeshProUGUI statPointText;
    public GameObject background;

    private PlayerStat_Manage _statSelection;

    private const float Duration = 0.2f;

    private bool _toggle = false;

    public AudioClip IncreaseSound { get { return _increaseSound; } }
    public AudioClip DecreaseSound { get { return _decreaseSound; } }
    public AudioClip ActionUnavailableSound { get { return _actionUnavailableSound; } }
    public override void SetUserInterface()
    {
        _statSelection = GetComponent<PlayerStat_Manage>();
        statPointText = transform.GetComponentInChild<TextMeshProUGUI>();

        _statSelection.Set(Managers.Data.user.Stat);

        for(int i = 0; i < _statElement_parent.childCount; i++)
        {
            _statElement_parent.GetChild(i).GetComponent<StatElementUpgrade_UI>().Set(files[i]);

            files[i].SetAction += _statSelection.Save;
        }

        background.SetActive(false);
    }
    public void TextUpdate()
    {
        statPointText.text = $"Stat Point : {Managers.Data.user.StatPoint}";
    }
    public void ToggleUI()
    {
        _toggle = !_toggle;

        background.SetActive(_toggle);
        
        if(_toggle)
        {
            transform.DOMove(new(0, 40), Duration)
                .SetEase(Ease.OutSine);
        }
        else
        {
            transform.DOMove(new(0, -1125), Duration)
                .SetEase(Ease.OutSine);
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StageIcon_UI : UserInterface
{
    [SerializeField] private TextMeshProUGUI _themaName;
    [SerializeField] private Image _ground;
    [SerializeField] private Image _cover;
    [SerializeField] private Image _banner;
    [SerializeField] private Image _monster;
    [SerializeField] private GameObject _padlock;

    private Stage_SO _so;

    private const string LockedText = "???";

    public override void SetUserInterface()
    {
        _themaName = transform.GetComponentInChildren<TextMeshProUGUI>();

        UpdateUI(0);
    }
    public void UpdateUI(int sign)
    {
        _so = Managers.Main.GetCurrentStageSO(sign);

        Set();
    }
    private void Set()
    {
        StageState state = Managers.Data.user.GetStageState();
        Icon_SO icon = _so.IconSprite;

        _ground.sprite = icon.Ground;
        _monster.sprite = icon.Monster;

        if(state == StageState.Locked)
        {
            _themaName.text = LockedText;
            _cover.color = Color.black;
            _ground.color = Color.black;
            _monster.color = Color.black;

            _banner.gameObject.SetActive(false);
            _monster.gameObject.SetActive(true);
            _padlock.SetActive(true);
        }
        else
        {
            _themaName.text = _so.Name;
            _cover.color = Color.white;
            _ground.color = Color.white;
            _monster.color = Color.white;

            if(state == StageState.Cleared)
            {
                _banner.sprite = icon.Banner;
                _banner.gameObject.SetActive(true);
                _monster.gameObject.SetActive(false);
            }
            else
            {
                _banner.gameObject.SetActive(false);
                _monster.gameObject.SetActive(true);
            }

            _padlock.SetActive(false);
        }

        if(icon.Cover == null)
        {
            _cover.gameObject.SetActive(false);
        }
        else
        {
            _cover.sprite = icon.Cover;

            _cover.gameObject.SetActive(true);
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StageIcon_UI : UserInterface
{
    [SerializeField] private TextMeshProUGUI themaName;
    [SerializeField] private Image ground;
    [SerializeField] private Image cover;
    [SerializeField] private Image banner;
    [SerializeField] private Image monster;
    [SerializeField] private GameObject padlock;

    private Stage_SO so;

    private const string LockedText = "???";

    public override void SetUserInterface()
    {
        themaName = transform.GetComponentInChildren<TextMeshProUGUI>();

        UpdateUI(0);
    }
    public void UpdateUI(int sign)
    {
        so = Managers.Main.GetCurrentStageSO(sign);

        Set();
    }
    private void Set()
    {
        StageState state = Managers.Data.user.GetStageState();
        Icon_SO icon = so.iconSprite;

        ground.sprite = icon.Ground;
        monster.sprite = icon.Monster;

        if(state == StageState.Locked)
        {
            themaName.text = LockedText;
            cover.color = Color.black;
            ground.color = Color.black;
            monster.color = Color.black;

            banner.gameObject.SetActive(false);
            monster.gameObject.SetActive(true);
            padlock.SetActive(true);
        }
        else
        {
            themaName.text = so.name;
            cover.color = Color.white;
            ground.color = Color.white;
            monster.color = Color.white;

            if(state == StageState.Cleared)
            {
                banner.sprite = icon.Banner;
                banner.gameObject.SetActive(true);
                monster.gameObject.SetActive(false);
            }
            else
            {
                banner.gameObject.SetActive(false);
                monster.gameObject.SetActive(true);
            }

            padlock.SetActive(false);
        }

        if(icon.Cover == null)
        {
            cover.gameObject.SetActive(false);
        }
        else
        {
            cover.sprite = icon.Cover;

            cover.gameObject.SetActive(true);
        }
    }
}
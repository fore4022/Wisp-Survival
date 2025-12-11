using System.Collections;
using UnityEngine;
/// <summary>
/// 메인 Scene 초기화
/// </summary>
public class Main_Scene : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        Managers.Audio.InitializedAudio();
        StartCoroutine(Initalizing());
    }
    private IEnumerator Initalizing()
    {
        int levelUpCount = 0;

        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        while(Managers.Data.user.Exp >= Managers.Data.UserExpTable.RequiredEXP[Managers.Data.user.Level - 1])
        {
            Managers.Data.user.Exp -= Managers.Data.UserExpTable.RequiredEXP[Managers.Data.user.Level - 1];
            Managers.Data.user.Level++;

            levelUpCount++;

            if(Managers.Data.user.Level == GameUtil.maxLevel)
            {
                break;
            }
        }

        if(levelUpCount != 0)
        {
            Managers.Data.user.StatPoint += levelUpCount;

            Managers.UI.ShowAndGet<UserLevelUp_UI>().PlayEffect(levelUpCount);
            Managers.UI.Get<UserExpSlider_UI>().UpdateExp();
            Managers.UI.Get<UserLevelText_UI>().LevelUpdate();
        }

        //if(!Managers.Data.user.Tutorial) 튜토리얼 구현 중
        //{
        //    if(levelUpCount != 0)
        //    {
        //        yield return new WaitUntil(() => !Managers.UI.Get<UserLevelUp_UI>().gameObject.activeSelf);
        //    }

        //    Managers.UI.Show<Tutorial_MaskImage_UI>();

        //    yield return null;

        //    yield return new WaitUntil(() => !Managers.UI.Get<UserLevelUp_UI>());

        //    Managers.UI.Show<Tutorial_MaskImage_UI>();
        //}
    }
}
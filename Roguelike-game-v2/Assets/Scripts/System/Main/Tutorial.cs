using System.Collections;
using UnityEngine;
public class Tutorial : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Initializing());
    }
    private IEnumerator Initializing()
    {
        yield return new WaitUntil(() => Managers.Data.user != null);

        if(!Managers.Data.user.Tutorial)
        {
            StartCoroutine(PlayingTutorial());
        }
    }
    private IEnumerator PlayingTutorial()
    {
        yield return new WaitUntil(() => true);

    }
}
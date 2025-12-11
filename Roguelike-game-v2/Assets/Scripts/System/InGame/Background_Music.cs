using System.Collections;
using UnityEngine;
/// <summary>
/// 인게임 배경음악 초기 설정
/// </summary>
public class Background_Music : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(Setting());
    }
    private IEnumerator Setting()
    {
        yield return new WaitUntil(() => Managers.Game.stageInformation != null);

        audioSource.clip = Managers.Game.stageInformation.BGM;
    }
}
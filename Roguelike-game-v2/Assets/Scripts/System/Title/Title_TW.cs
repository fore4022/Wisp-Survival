using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 타이틀 화면 Tween 연출
/// </summary>
public class Title_TW : MonoBehaviour
{
    [SerializeField] private List<Transform> tweens;
    [SerializeField] private SpriteRenderer _explosion;

    private void Start()
    {
        StartCoroutine(ReOrder());

        // Wisp
        tweens[0].SetScale(23, 1.2f, 0.1f, EaseType.OutExpo)
            .SetPosition(new(-0.15f, 0.675f), 0.5f, 0.075f, EaseType.OutCirc)
            .SetRotation(new(0, 0, 727.5f), 1.1f, 0.035f, EaseType.OutQuint);

        // Moth
        tweens[1].SetScale(3, 0.6f, 0.15f, EaseType.OutExpo)
            .SetPosition(new(-2f, -5f), 0.5f, 0.2f, EaseType.OutSine)
            .SetRotation(new(0, 0, -20), 1f, 0.2f, EaseType.OutExpo);

        // Cloud
        tweens[2].SetScale(2.5f, 0.7f, 0.25f, EaseType.OutExpo)
            .SetPosition(new(2.2f, -4.05f), 0.65f, 0.25f, EaseType.OutCirc)
            .SetRotation(new(0, 0, 7.5f), 1f, 0.25f, EaseType.OutExpo);

        // SlimeSquare
        tweens[3].SetScale(3, 0.9f, 0.35f, EaseType.OutExpo)
            .SetPosition(new(-2.9f, -0.8f), 1f, 0.35f, EaseType.OutBack)
            .SetRotation(new(0, 0, 1120), 1.1f, 0.3f, EaseType.OutExpo);

        // BatSmallA
        tweens[4].SetScale(2.75f, 0.9f, 0.4f, EaseType.OutExpo)
            .SetPosition(new(-3.5f, 0.6f), 1f, 0.375f, EaseType.OutBack)
            .SetRotation(new(0, 0, 740), 1.1f, 0.3f, EaseType.OutExpo);

        // Mushroom_1
        tweens[5].SetScale(2, 1.05f, 0.3f, EaseType.OutExpo)
            .SetPosition(new(3.5f, 6.1f), 0.95f, 0.35f, EaseType.OutExpo)
            .SetRotation(new(0, 0, 390), 1.15f, 0.25f, EaseType.OutExpo);

        // Mushroom_2
        tweens[6].SetScale(3, 1.2f, 0.35f, EaseType.OutExpo)
            .SetPosition(new(2.15f, -2.55f), 0.8f, 0.35f, EaseType.OutQuart)
            .SetRotation(new(0, 0, 155), 1f, 0.35f, EaseType.OutQuart);

        // PotionI
        tweens[7].SetScale(3, 1.2f, 0.45f, EaseType.OutExpo)
            .SetPosition(new(-3f, 6.75f), 0.8f, 0.35f, EaseType.OutQuart)
            .SetRotation(new(0, 0, 405f), 1f, 0.3f, EaseType.OutQuad);

        // Sword
        tweens[8].SetScale(4, 1.2f, 0.35f, EaseType.OutExpo)
            .SetPosition(new(-1.7f, 6.3f), 0.8f, 0.35f, EaseType.OutExpo)
            .SetRotation(new(0, 0, 752.5f), 1.1f, 0.25f, EaseType.OutExpo);

        // Mask
        tweens[9].SetScale(3.2f, 1.2f, 0.35f, EaseType.OutQuint)
            .SetPosition(new(3f, -7f), 0.8f, 0.35f, EaseType.OutQuint)
            .SetRotation(new(0, 0, 15), 1.1f, 0.3f, EaseType.OutQuint);
    }
    private IEnumerator ReOrder()
    {
        Animator Animator = _explosion.GetComponent<Animator>();

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _explosion.sortingOrder = 0;
    }
}
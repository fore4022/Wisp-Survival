using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 타이틀 화면 Tween 연출
/// </summary>
public class Title_TW : MonoBehaviour
{
    [SerializeField] private List<Transform> _entityList;
    [SerializeField] private SpriteRenderer _explosion;

    private void Start()
    {
        StartCoroutine(ReOrder());

        // Wisp
        _entityList[0].DOScale(23f, 1.2f).SetDelay(0.1f).SetEase(Ease.OutExpo);
        _entityList[0].DOMove(new(-0.15f, 0.675f), 0.5f).SetDelay(0.075f).SetEase(Ease.OutCirc);
        _entityList[0].DORotate(new(0, 0, 727.5f), 1.1f).SetDelay(0.035f).SetEase(Ease.OutQuint);

        // Moth
        _entityList[1].DOScale(3f, 0.6f).SetDelay(0.15f).SetEase(Ease.OutExpo);
        _entityList[1].DOMove(new(-2f, -5f), 0.5f).SetDelay(0.2f).SetEase(Ease.OutSine);
        _entityList[1].DORotate(new(0, 0, -20f), 1f).SetDelay(0.2f).SetEase(Ease.OutExpo);

        // Cloud
        _entityList[2].DOScale(2.5f, 0.7f).SetDelay(0.25f).SetEase(Ease.OutExpo);
        _entityList[2].DOMove(new(2.2f, -4.05f), 0.65f).SetDelay(0.25f).SetEase(Ease.OutCirc);
        _entityList[2].DORotate(new(0, 0, 7.5f), 0.25f).SetDelay(0.25f).SetEase(Ease.OutExpo);

        // SlimeSquare
        _entityList[3].DOScale(3f, 0.9f).SetDelay(0.35f).SetEase(Ease.OutExpo);
        _entityList[3].DOMove(new(-2.9f, -0.8f), 1f).SetDelay(0.35f).SetEase(Ease.OutBack);
        _entityList[3].DORotate(new(0, 0, 1120), 1.1f).SetDelay(0.3f).SetEase(Ease.OutExpo);

        // BatSmallA
        _entityList[4].DOScale(2.75f, 0.9f).SetDelay(0.4f).SetEase(Ease.OutExpo);
        _entityList[4].DOMove(new(-3.5f, 0.6f), 1f).SetDelay(0.375f).SetEase(Ease.OutBack);
        _entityList[4].DORotate(new(0, 0, 740), 1.1f).SetDelay(0.3f).SetEase(Ease.OutExpo);

        // Mushroom_1
        _entityList[5].DOScale(2, 1.05f).SetDelay(0.3f).SetEase(Ease.OutExpo);
        _entityList[5].DOMove(new(3.5f, 6.1f), 0.95f).SetDelay(0.35f).SetEase(Ease.OutExpo);
        _entityList[5].DORotate(new(0, 0, 390), 1.15f).SetDelay(0.25f).SetEase(Ease.OutExpo);

        // Mushroom_2
        _entityList[6].DOScale(3, 1.2f).SetDelay(0.35f).SetEase(Ease.OutExpo);
        _entityList[6].DOMove(new(2.15f, -2.55f), 0.8f).SetDelay(0.35f).SetEase(Ease.OutQuart);
        _entityList[6].DORotate(new(0, 0, 155), 1).SetDelay(0.35f).SetEase(Ease.OutQuart);

        // PotionI
        _entityList[7].DOScale(3, 1.2f).SetDelay(0.45f).SetEase(Ease.OutExpo);
        _entityList[7].DOMove(new(-3f, 6.75f), 0.8f).SetDelay(0.35f).SetEase(Ease.OutQuart);
        _entityList[7].DORotate(new(0, 0, 405), 1).SetDelay(0.3f).SetEase(Ease.OutQuad);

        // Sword
        _entityList[8].DOScale(4, 1.2f).SetDelay(0.35f).SetEase(Ease.OutExpo);
        _entityList[8].DOMove(new(-1.7f, 6.3f), 0.8f).SetDelay(0.35f).SetEase(Ease.OutExpo);
        _entityList[8].DORotate(new(0, 0, 752.5f), 1.1f).SetDelay(0.25f).SetEase(Ease.OutExpo);

        // Mask
        _entityList[9].DOScale(3.2f, 1.2f).SetDelay(0.35f).SetEase(Ease.OutQuint);
        _entityList[9].DOMove(new(3, -7f), 0.8f).SetDelay(0.35f).SetEase(Ease.OutQuint);
        _entityList[9].DORotate(new(0, 0, 15), 1.1f).SetDelay(0.3f).SetEase(Ease.OutQuint);
    }
    private IEnumerator ReOrder()
    {
        Animator Animator = _explosion.GetComponent<Animator>();

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _explosion.sortingOrder = 0;
    }
}
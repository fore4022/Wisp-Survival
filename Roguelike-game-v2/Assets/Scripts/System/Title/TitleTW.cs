using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 타이틀 화면 Tween 연출
/// </summary>
public class TitleTW : MonoBehaviour
{
    [SerializeField] private List<Transform> _entityList;
    [SerializeField] private SpriteRenderer _explosion;
    [SerializeField] private float _floatAmount = 0.175f;
    [SerializeField] private float _scaleAmount = 1.025f;
    [SerializeField] private float _rotationAmount = 3;
    [SerializeField] private float _duration = 2;

    private bool _canEnterMain = false;

    public bool CanEnterMain { get { return _canEnterMain; } }
    private void Start()
    {
        // Wisp
        _entityList[0].DOScale(23f, 1.2f)
            .SetDelay(0.1f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[0]);
            });

        _entityList[0].DOMove(new(-0.15f, 0.675f), 0.5f)
            .SetDelay(0.075f)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[0]);
            });

        _entityList[0].DORotate(new(0, 0, 727.5f), 1.1f, RotateMode.FastBeyond360)
            .SetDelay(0.035f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                DORotate(_entityList[0]);
            });

        // Moth
        _entityList[1].DOScale(3f, 0.6f)
            .SetDelay(0.15f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[1]);
            });

        _entityList[1].DOMove(new(-2f, -5f), 0.5f)
            .SetDelay(0.2f)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[1]);
            });

        _entityList[1].DORotate(new(0, 0, -20f), 1f)
            .SetDelay(0.2f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DORotate(_entityList[1]);
            });

        // Cloud
        _entityList[2].DOScale(2.5f, 0.7f)
            .SetDelay(0.25f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[2]);
            });

        _entityList[2].DOMove(new(2.2f, -4.05f), 0.65f)
            .SetDelay(0.25f)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[2]);
            });

        _entityList[2].DORotate(new(0, 0, 7.5f), 0.25f)
            .SetDelay(0.25f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DORotate(_entityList[2]);
            });

        // SlimeSquare
        _entityList[3].DOScale(3f, 0.9f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[3]);
            });

        _entityList[3].DOMove(new(-2.9f, -0.8f), 1f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[3]);
            });

        _entityList[3].DORotate(new(0, 0, 1120), 1.1f, RotateMode.FastBeyond360)
            .SetDelay(0.3f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DORotate(_entityList[3]);
            });

        // BatSmallA
        _entityList[4].DOScale(2.75f, 0.9f)
            .SetDelay(0.4f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[4]);
            });

        _entityList[4].DOMove(new(-3.5f, 0.6f), 1f)
            .SetDelay(0.375f)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[4]);
            });

        _entityList[4].DORotate(new(0, 0, 740), 1.1f, RotateMode.FastBeyond360)
            .SetDelay(0.3f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DORotate(_entityList[4]);
            });

        // Mushroom_1
        _entityList[5].DOScale(2, 1.05f)
            .SetDelay(0.3f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[5]);
            });

        _entityList[5].DOMove(new(3.05f, 1.25f), 0.95f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[5]);
            });

        _entityList[5].DORotate(new(0, 0, 390), 1.15f, RotateMode.FastBeyond360)
            .SetDelay(0.25f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DORotate(_entityList[5]);
            });

        // Mushroom_2
        _entityList[6].DOScale(3, 1.2f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[6]);
            });

        _entityList[6].DOMove(new(2.15f, -2.55f), 0.8f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[6]);
            });

        _entityList[6].DORotate(new(0, 0, 155), 1)
            .SetDelay(0.35f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                DORotate(_entityList[6]);
            });

        // PotionI
        _entityList[7].DOScale(3, 1.2f)
            .SetDelay(0.45f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[7]);
            });

        _entityList[7].DOMove(new(2.6f, -0.3f), 0.8f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[7]);
            });

        _entityList[7].DORotate(new(0, 0, 405), 1, RotateMode.FastBeyond360)
            .SetDelay(0.3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                DORotate(_entityList[7]);
            });

        // Sword
        _entityList[8].DOScale(4, 1.2f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOScale(_entityList[8]);
            });

        _entityList[8].DOMove(new(-2, -2.55f), 0.8f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[8]);
            });

        _entityList[8].DORotate(new(0, 0, 752.5f), 1.1f, RotateMode.FastBeyond360)
            .SetDelay(0.25f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                DORotate(_entityList[8]);
            });

        // Mask
        _entityList[9].DOScale(3.2f, 1.2f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                DOScale(_entityList[9]);
            });

        _entityList[9].DOMove(new(3, -7f), 0.8f)
            .SetDelay(0.35f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                DOMoveY(_entityList[9]);
            });

        _entityList[9].DORotate(new(0, 0, 15), 1.1f)
            .SetDelay(0.3f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                DORotate(_entityList[9]);
            });

        StartCoroutine(ReOrder());
    }
    public void AllSkipToEnd()
    {
        foreach(Transform tr in _entityList)
        {
            DOTween.Complete(tr);
        }
    }
    private float RandomDuration()
    {
        return Random.Range(0.1f, 0.4f);
    }
    private void DOScale(Transform trans)
    {
        trans.DOScale(trans.localScale * _scaleAmount, _duration + RandomDuration())
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
    private void DOMoveY(Transform trans)
    {
        trans.DOMoveY(trans.position.y + _floatAmount, _duration + RandomDuration())
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
    private void DORotate(Transform trans)
    {
        trans.DORotate(new(0, 0, _rotationAmount), _duration + RandomDuration(), RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
    private IEnumerator ReOrder()
    {
        Animator Animator = _explosion.GetComponent<Animator>();
        Transform trans = _explosion.transform;

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        _explosion.sortingOrder = 0;
        _canEnterMain = true;

        DOScale(trans);
        DOMoveY(trans);
        DORotate(trans);
    }
}
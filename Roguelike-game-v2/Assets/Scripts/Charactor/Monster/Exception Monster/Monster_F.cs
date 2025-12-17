using System.Collections;
using UnityEngine;
/// <summary>
/// 사망할 경우 작아진 객체로 분열하며, 분열된 객체는 추가 분열되지 않음
/// 분열된 객체는 경험치를 지급하지 않음
/// </summary>
/// <remarks>
/// 사용 객체 : SlimeSquareH
/// </remarks>
public class Monster_F : BasicMonster_WithObject
{
    [SerializeField][Min(0.25f)] private float _defaultScaleValue;
    [SerializeField][Min(0.1f)] private float _splitScale;
    [SerializeField][Min(2)] private float _splitInstanceCount;
    [SerializeField][Range(0, 100)] private float _skillCastChance;

    private Vector3 _defaultScale;
    private string _monsterKey;
    private float _adjustmentScale;

    private bool IsSplite { get { return transform.localScale.x == _splitScale; } }
    // 크기 값 및 몬스터 키 초기화
    protected override void Init()
    {
        transform.localScale = _defaultScale = new(_defaultScaleValue, _defaultScaleValue);
        _monsterKey = monsterSO.ExtraObjects[0].name;
        _adjustmentScale = _defaultScaleValue / 20;

        base.Init();
    }
    // 분열되지 않은 객체 위치 설정
    protected override void SetPosition()
    {
        if(!IsSplite)
        {
            base.SetPosition();
        }
    }
    // 분열된 객체일 경우 FlipX 실행
    protected override void Enable()
    {
        if(IsSplite)
        {
            FlipX();
        }

        base.Enable();
    }
    // 분열되지 않은 객체 사망시, 확률적으로 분열
    protected override void Die()
    {
        if(!IsSplite)
        {
            if(Random.Range(0, 100) <= _skillCastChance)
            {
                _user_Experience = monsterSO.User_Experience;

                StartCoroutine(RepeatBehavior());

                return;
            }
        }
        else
        {
            _user_Experience = 0;
        }

        base.Die();
    }
    // 분열과 상관 없이 기본 크기로 설정
    private void OnDisable()
    {
        transform.localScale = _defaultScale;
    }
    // 사망 효과의 특정 시점까지 대기, 몬스터 키를 통해서 크기와 경험치를 재설정한 분열된 객체 생성
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f);

        PoolingObject go;

        for(int i = 0; i < _splitInstanceCount; i++)
        {
            go = Managers.Game.objectPool.GetObject(_monsterKey);
            go.Transform.localScale = new Vector2(_splitScale, _splitScale);
            go.Transform.position = transform.position + new Vector3(_adjustmentScale * Random.Range(-1, 2), _adjustmentScale * Random.Range(-1, 2));

            go.SetActive(true);

            yield return null;
        }

        base.Die();
    }
}
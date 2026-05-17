using System.Collections;
using UnityEngine;

/// <summary>
/// 일정한 확률로 화면의 무작위 위치에 투사체 발사
/// </summary>
/// <remarks>
/// 사용 객체 : MothD, PuddleB
/// </remarks>

public class MonsterB : BasicMonster_WithObject
{
    [SerializeField] private float _coolTime = 3f;
    [SerializeField][Range(0, 100)] private float _skillCastChance;

    private Coroutine _behavior = null;
    private WaitForSeconds _delay;
    private string _visualizerKey;
    private string _skillKey;

    protected override void Init()
    {
        _delay = new(_coolTime);
        _skillKey = monsterSO.ExtraObjects[0].name;
        _visualizerKey = monsterSO.ExtraObjects[1].name;

        base.Init();
    }

    protected override void Enable()
    {
        base.Enable();

        _behavior = StartCoroutine(RepeatBehavior());
    }

    protected override void Die()
    {
        base.Die();

        StopCoroutine(_behavior);
    }

    private IEnumerator RepeatBehavior()
    {
        while(true)
        {
            yield return _delay;

            if(Random.Range(0, 100) <= _skillCastChance)
            {
                if((Managers.Game.player.transform.position - transform.position).magnitude <= CameraUtil.CameraHeight / 2)
                {
                    CoroutineHelper.Start(SkillCasting(), CoroutineType.InGameSystem);
                }
            }
        }
    }

    private IEnumerator SkillCasting()
    {
        PoolingObject visualizer = Managers.Game.objectPool.GetObject(_visualizerKey);
        PoolingObject skill = Managers.Game.objectPool.GetObject(_skillKey);
        Vector3 position = MonsterDetection.GetRandomVector();

        visualizer.Transform.position = position;
        skill.Transform.position = position;

        visualizer.SetActive(true);
        skill.SetActive(true);

        yield return new WaitUntil(() => !skill.ActiveSelf);

        visualizer.SetActive(false);
    }
}
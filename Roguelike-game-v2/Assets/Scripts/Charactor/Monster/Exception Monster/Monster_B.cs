using System.Collections;
using UnityEngine;
/// <summary>
/// 일정한 확률로 화면의 무작위 위치에 투사체 발사
/// </summary>
public class Monster_B : BasicMonster_WithObject
{
    [SerializeField] private float coolTime = 3f;
    [SerializeField][Range(0, 100)] private float skillCastChance;

    private Coroutine behavior = null;
    private WaitForSeconds delay;
    private string visualizerKey;
    private string skillKey;

    protected override void Init()
    {
        delay = new(coolTime);
        skillKey = monsterSO.ExtraObjects[0].name;
        visualizerKey = monsterSO.ExtraObjects[1].name;

        base.Init();
    }
    protected override void Enable()
    {
        base.Enable();

        behavior = StartCoroutine(RepeatBehavior());
    }
    protected override void Die()
    {
        base.Die();

        StopCoroutine(behavior);
    }
    private IEnumerator RepeatBehavior()
    {
        while(true)
        {
            yield return delay;

            if(Random.Range(0, 100) <= skillCastChance)
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
        PoolingObject visualizer = Managers.Game.objectPool.GetObject(visualizerKey);
        PoolingObject skill = Managers.Game.objectPool.GetObject(skillKey);
        Vector3 position = MonsterDetection.GetRandomVector();

        visualizer.Transform.position = position;
        skill.Transform.position = position;

        visualizer.SetActive(true);
        skill.SetActive(true);

        yield return new WaitUntil(() => !skill.activeSelf);

        visualizer.SetActive(false);
    }
}
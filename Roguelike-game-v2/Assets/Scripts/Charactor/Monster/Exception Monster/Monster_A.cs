using System.Collections;
using UnityEngine;
/// <summary>
/// 플레이어에게 원거리 투사체를 발사
/// </summary>
public class Monster_A : BasicMonster_WithObject
{
    [SerializeField] private float coolTime = 2.5f;

    private Coroutine behavior = null;
    private WaitForSeconds delay;
    private string skillKey;

    protected override void Init()
    {
        delay = new(coolTime);
        skillKey = monsterSO.ExtraObjects[0].name;

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
        PoolingObject go;

        while(true)
        {
            yield return delay;

            if((Managers.Game.player.transform.position - transform.position).magnitude <= CameraUtil.CameraHeight / 2)
            {
                go = Managers.Game.objectPool.GetObject(skillKey);
                go.Transform.position = transform.position;

                go.SetActive(true);
            }
        }
    }
}
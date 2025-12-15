using System.Collections;
using UnityEngine;
/// <summary>
/// 플레이어에게 원거리 투사체를 발사
/// </summary>
/// <remarks>
/// 사용 객체 : 
/// </remarks>
public class Monster_A : BasicMonster_WithObject
{
    [SerializeField] private float _coolTime = 2.5f;

    private Coroutine _behavior = null;
    private WaitForSeconds _delay;
    private string _skillKey;

    protected override void Init()
    {
        _delay = new(_coolTime);
        _skillKey = monsterSO.ExtraObjects[0].name;

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
        PoolingObject go;

        while(true)
        {
            yield return _delay;

            if((Managers.Game.player.transform.position - transform.position).magnitude <= CameraUtil.CameraHeight / 2)
            {
                go = Managers.Game.objectPool.GetObject(_skillKey);
                go.Transform.position = transform.position;

                go.SetActive(true);
            }
        }
    }
}
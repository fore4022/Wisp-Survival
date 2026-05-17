using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 투사체 형식의 스킬의 기본 구현
/// </para>
/// 카메라 시야 범위에 보이지 않을 경우 ObjectPool로 회수 기능 구현
/// </summary>
public class PlayerSkillProjectile : PlayerSkill
{
    protected Coroutine moving = null;
    protected Vector3 direction;

    private const float collectDelay = 3;

    private Plane[] planes = new Plane[6];
    private Coroutine collect = null;
    private WaitForSeconds delay = new(collectDelay);

    protected void FixedUpdate()
    {
        IsInvisible();
    }
    private void IsInvisible()
    {
        if(_baseCast == null)
        {
            return;
        }

        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(planes, _defaultCollider.bounds))
        {
            if(collect != null)
            {
                StopCoroutine(collect);

                collect = null;
            }
        }
        else
        {
            if(collect == null)
            {
                collect = StartCoroutine(Collecting());
            }
        }
    }
    private IEnumerator Collecting()
    {
        yield return delay;

        collect = _baseCast = null;
    }
}
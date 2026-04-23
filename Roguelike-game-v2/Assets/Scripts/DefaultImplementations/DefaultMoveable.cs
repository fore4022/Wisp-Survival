using System.Collections;
using UnityEngine;

/// <summary>
/// IMoveableÀÇ ±¸Çö
/// </summary>

public class DefaultMoveable : IMoveable, IDefaultImplementable
{
    private MonoBehaviour _mono;

    private float _slowDown = 0;

    public float SpeedAmount { get; }

    public float SlowDownAmount
    {
        get
        {
            if(_slowDown == 0)
            {
                return 1;
            }

            return 1 - (_slowDown / (_slowDown + 100));
        }
    }

    public IDefaultImplementable Set(Transform transform)
    {
        _mono = transform.GetComponent<MonoBehaviour>();

        return this;
    }

    public void OnMove() { }

    public void SetSlowDown(float slowDown, float duration)
    {
        _mono.StartCoroutine(HandleSlow(slowDown, duration));
    }

    public IEnumerator HandleSlow(float slowDown, float duration)
    {
        _slowDown += slowDown;

        yield return new WaitForSeconds(duration);

        _slowDown -= slowDown;
    }
}
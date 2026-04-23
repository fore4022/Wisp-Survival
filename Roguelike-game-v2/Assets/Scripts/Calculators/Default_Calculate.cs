using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여러가지 기능 구현에 필요한 연산 구현
/// </summary>

public static class Default_Calculate
{
    public static float width = CameraUtil.CameraWidth / 2 - _offset;
    public static float height = CameraUtil.CameraHeight / 2 - _offset;

    private static float _offset = 0.5f;

    public static Vector3 GetDirection(Vector3 targetPosition)
    {
        return (targetPosition - Managers.Game.player.gameObject.transform.position).normalized;
    }

    public static Vector3 GetDirection(Vector3 targetPosition, Vector3 currentPosition)
    {
        return (targetPosition - currentPosition).normalized;
    }

    public static Vector3 GetDirection(Vector3 targetPosition, Vector3 position, bool isNormalized = true)
    {
        if(targetPosition == null || position == null || targetPosition == position)
        {
            return GetRandomDirection();
        }

        if(isNormalized)
        {
            return (targetPosition - position).normalized;
        }
        else
        {
            return (targetPosition - position);
        }
    }

    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-360, 361), Random.Range(-360, 361), 0).normalized;
    }

    public static Vector2 GetRandomVector()
    {
        float x = Random.Range(-width, width);
        float y = Random.Range(-height, height);

        return new(x, y);
    }

    public static Quaternion GetQuaternion(Vector3 direction)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public static Quaternion GetQuaternion(Vector3 direction, Vector3 baseRotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + baseRotation.z);
    }

    public static Quaternion GetRandomQuaternion()
    {
        Vector2 vec = GetRandomVector();

        return Quaternion.Euler(0, 0, Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg);
    }

    public static int[] GetRandomValues(int maxValue, int count = 0)
    {
        List<int> valueList = new();
        int[] result;
        int index;

        for(int i = 0; i < maxValue; i++)
        {
            valueList.Add(i);
        }

        if(count != 0)
        {
            result = new int[count];

            for(int i = 0; i < count; i++)
            {
                index = Random.Range(0, maxValue - i);
                result[i] = valueList[index];

                valueList.RemoveAt(index);
            }
        }
        else
        {
            result = new int[maxValue];

            for(int i = 0; i < maxValue; i++)
            {
                index = Random.Range(0, maxValue - i);
                result[i] = valueList[index];

                valueList.RemoveAt(index);
            }
        }

        return result;
    }

    public static float GetParabolicY(float size, float peakValue, float value)
    {
        if(peakValue == 0 || size == 0)
        {
            return 0;
        }

        return peakValue * (Mathf.Pow((value - size / 2), 2) / Mathf.Pow(size / 2, 2));
    }
}
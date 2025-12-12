using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 플레이어 스킬 대상을 구하는 식 구현
/// </summary>
public static class MonsterDetection
{
    public static float largeastRange = 1.15f;

    private static Vector2 _vec = new();

    public static Vector2 GetNearestMonsterPosition(Transform transform = null)
    {
        GameObject target = FindNearestMonster(transform);

        if(target == null)
        {
            return GetRandomVector();
        }
        else
        {
            return target.transform.position;
        }
    }
    public static Vector2 GetRandomMonsterPosition()
    {
        GameObject target = FindRandomMonster();

        if(target == null)
        {
            return GetRandomVector();
        }
        else
        {
            return target.transform.position;
        }
    }
    public static Vector2 GetLargestMonsterGroup()
    {
        GameObject target = FindLargestMonsterGroup();

        if(target == null)
        {
            return GetRandomVector();
        }
        else
        {
            return target.transform.position;
        }
    }
    public static List<Vector2> GetLargestMonsterGroup(int count)
    {
        List<GameObject> targetList = FindLargestMonsterGroup(count);
        List<Vector2> targetPositionList = new List<Vector2>();

        if(targetList.Count == 0)
        {
            for(int i = 0; i < count; i++)
            {
                targetPositionList.Add(GetRandomVector());
            }
        }
        else
        {
            if(targetList.Count > count)
            {
                count -= targetList.Count;

                for(int i = 0; i < count; i++)
                {
                    targetPositionList.Add(GetRandomVector());
                }
            }

            foreach(GameObject target in targetList)
            {
                targetPositionList.Add(target.transform.position);
            }
        }

        return targetPositionList;
    }
    public static Vector2 GetRandomVector()
    {
        _vec.x = Random.Range(-Default_Calculate.width, Default_Calculate.width);
        _vec.y = Random.Range(-Default_Calculate.height, Default_Calculate.height);

        return _vec + (Vector2)Managers.Game.player.gameObject.transform.position;
    }
    private static List<GameObject> FindLargestMonsterGroup(int count)
    {
        List<(GameObject obj, int enemyCount) > targetObjectList = new List<(GameObject obj, int enemyCount)>();
        List<GameObject> gameObjectList = FindMonstersOnScreen();

        int enemyCount;

        foreach(GameObject go in gameObjectList)
        {
            enemyCount = Physics2D.OverlapCircleAll(go.transform.position, largeastRange).Count();

            targetObjectList.Add((go, enemyCount));
        }

        targetObjectList.OrderByDescending(tuple => tuple.enemyCount).ToList();

        gameObjectList = new List<GameObject>();

        foreach((GameObject obj, int enemyCount) value in targetObjectList)
        {
            gameObjectList.Add(value.obj);
        }

        return gameObjectList.Take(count).ToList();
    }
    private static List<GameObject> FindMonstersOnScreen(float? range = null)
    {
        List<GameObject> resultList = new List<GameObject>();
        Collider2D[] colliderArray;
        
        Vector2 radiusPosition = Camera.main.transform.position;

        if(range == null)
        {
            float y = Camera.main.orthographicSize * 2;
            float x = y * Camera.main.aspect;

            Vector2 radius = new(x, y);

            colliderArray = Physics2D.OverlapBoxAll(radiusPosition, radius, 0, LayerMask.GetMask("Monster"));
        }
        else
        {
            colliderArray = Physics2D.OverlapCircleAll(radiusPosition, (float)range, 0, LayerMask.GetMask("Monster"));
        }

        foreach(Collider2D col in colliderArray)
        {
            resultList.Add(col.gameObject);
        }

        return resultList;
    }
    private static GameObject FindRandomMonster()
    {
        List<GameObject> gameObjectList = FindMonstersOnScreen();

        if(gameObjectList.Count == 0)
        {
            return null;
        }

        int index = Random.Range(0, gameObjectList.Count);

        return gameObjectList[index];
    }
    private static GameObject FindNearestMonster(Transform transform = null)
    {
        List<GameObject> gameObjectList = FindMonstersOnScreen();

        GameObject targetObject = null;

        float minDistance = 0;

        foreach(GameObject go in gameObjectList)
        {
            if(targetObject == null)
            {
                GetDistance(go, out minDistance, transform);

                targetObject = go;
            }
            else if(minDistance > GetDistance(go, out float distance, transform))
            {
                minDistance = distance;
                targetObject = go;
            }
        }

        if(minDistance == 0)
        {
            return null;
        }

        return targetObject;
    }
    private static GameObject FindLargestMonsterGroup()
    {
        List<GameObject> gameObjectList = FindMonstersOnScreen();
        Collider2D[] colliderArray;

        GameObject targetObject = null;

        int maxIntCount = 0;
        int count;

        foreach(GameObject go in gameObjectList)
        {
            colliderArray = Physics2D.OverlapCircleAll(go.transform.position, largeastRange);
            count = colliderArray.Length;

            if(maxIntCount < count)
            {
                maxIntCount = count;
                targetObject = go;
            }
        }

        if(maxIntCount == 0)
        {
            return null;
        }

        return targetObject;
    }
    private static float GetDistance(GameObject go, out float result, Transform transform = null)
    {
        float distance = 0;

        if(transform == null)
        {
            distance = (go.transform.position - Managers.Game.player.gameObject.transform.position).magnitude;
        }
        else
        {
            distance = (go.transform.position - transform.position).magnitude;
        }

        return result = distance;
    }
}
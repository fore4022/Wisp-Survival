using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// <para>
/// 오브젝트 풀
/// </para>
/// 프리팹의 인스턴스에 대한 생성과 제어
/// </summary>
public class ObjectPool
{
    private Dictionary<string, List<PoolingObject>> poolingObjects = new();

    private Transform root;

    private const int maxWorkPerFrame = 360;
    private const int defaultObjectCount = 500;

    private int coroutineCount = 0;

    // 오브젝트 풀이 생성될 때, 풀링 되는 객체들이 위치할 root 
    public ObjectPool()
    {
        GameObject go = GameObject.Find("@ObjectPool");

        if(go == null)
        {
            go = new GameObject { name = "@ObjectPool" };
        }

        root = go.transform;
    }
    public Dictionary<string, List<PoolingObject>> PoolingObjects { get { return poolingObjects; } }
    public int PoolingObjectsCount { get { return poolingObjects.Count; } }
    // 프레임당 생성량 반환
    private int MaxWorkPerSec { get { return Mathf.Max(maxWorkPerFrame / coroutineCount, 1); } }
    // 키에 해당하는 오브젝트 활성화
    public PoolingObject ActiveObject(string prefabKey)
    {
        PoolingObject go = GetObject(prefabKey, false);

        go.SetActive(true);

        return go;
    }
    // 키에 해당하는 리스트에서, 입력 받은 오브젝트를 가지는 PoolingObject isUsed와 오브젝트 비활성화
    public void DisableObject(GameObject prefab, string key)
    {
        poolingObjects.TryGetValue(key, out List<PoolingObject> objs);

        objs.Find(o => o.GameObject == prefab).isInUse = false;

        prefab.SetActive(false);
    }
    // 키에 해당하는 오브젝트 반환, 활성화 여부 지정 가능
    public PoolingObject GetObject(string prefabKey, bool setInUse = true)
    {
        foreach(PoolingObject obj in poolingObjects[prefabKey])
        {
            if(!obj.GameObject.activeSelf && (!obj.isInUse || obj.isUsed))
            {
                if(setInUse)
                {
                    obj.isInUse = true;
                }

                obj.isUsed = false;

                return obj;
            }
        }

        return null;
    }
    // 키에 해당하는 오브젝트 전부 반환
    public List<PoolingObject> GetObjects(string prefabKey)
    {
        if(poolingObjects.ContainsKey(prefabKey))
        {
            return poolingObjects[prefabKey];
        }

        return null;
    }
    // 모든 PoolingObject를 초기 상태로 설정,isUsed와 오브젝트 비활성화
    public void ReSetting()
    {
        foreach(List<PoolingObject> objList in poolingObjects.Values)
        {
            foreach(PoolingObject obj in objList)
            {
                if(obj.GameObject.activeSelf)
                {
                    obj.GameObject.SetActive(false);
                }

                if(obj.isInUse)
                {
                    obj.isInUse = false;
                }
            }
        }
    }
    // 프리팹을 개수만큼 생성
    public void Create(GameObject prefab, int count = defaultObjectCount)
    {
        CoroutineHelper.Start(CreatingInstance(prefab, count, false), CoroutineType.InGameSystem);
    }
    // 프리팹 항목들을 개수만큼 생성
    public void Create(List<GameObject> prefabs, int count = defaultObjectCount)
    {
        foreach(GameObject prefab in prefabs)
        {
            CoroutineHelper.Start(CreatingInstance(prefab, count), CoroutineType.InGameSystem);
        }
    }
    // 오브젝트 풀로 생성된 오든 오브젝트의 코루틴 중단
    public void StopAllActions()
    {
        foreach(List<PoolingObject> objs in poolingObjects.Values)
        {
            foreach(PoolingObject obj in objs)
            {
                if(obj.activeSelf)
                {
                    obj.StopAllCoroutines();
                }
            }
        }
    }
    // 프리팹을 _root의 자식으로 개수만큼 생성
    private void CreateInstance(Transform _root, GameObject prefab, int count, int instanceCount, ref GameObject[] array)
    {
        for(int i = 0; i < count; i++)
        {
            array[instanceCount + i] = Object.Instantiate(prefab, _root);
            array[instanceCount + i].SetActive(false);
        }
    }
    // 인스턴스가 위치할 root를 생성 및 poolingObjects에 등록 또는 할당
    private IEnumerator CreatingInstance(GameObject prefab, int count, bool isSetRoot = true)
    {
        GameObject[] array = new GameObject[count];

        string key = prefab.name;

        GameObject parent = GameObject.Find(key);
        Transform transform;

        if(parent == null)
        {
            transform = new GameObject { name = key }.transform;
        }
        else
        {
            transform = parent.transform;
        }

        yield return new WaitUntil(() => transform != null);

        int instanceCount = 0;
        int createCount;

        if(isSetRoot)
        {
            transform.SetParent(root);
        }

        coroutineCount++;

        while(instanceCount < count)
        {
            createCount = Mathf.Min(MaxWorkPerSec, count - instanceCount);

            CreateInstance(transform, prefab, createCount, instanceCount, ref array);

            instanceCount += createCount;

            yield return null;
        }

        if(!poolingObjects.ContainsKey(key))
        {
            poolingObjects.Add(key, new());
        }

        for(int i = 0; i < array.Count(); i++)
        {
            poolingObjects[key].Add(new(array[i]));
        }

        coroutineCount--;
    }
}
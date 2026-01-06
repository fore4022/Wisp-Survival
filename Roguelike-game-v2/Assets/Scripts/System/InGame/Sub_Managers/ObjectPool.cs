using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>
/// 프리팹의 인스턴스에 대한 생성과 제어
/// </para>
/// </summary>
public class ObjectPool
{
    private Dictionary<string, List<PoolingObject>> _poolingObjects = new();

    private Transform _root;

    private const int MaxWorkPerFrame = 20;
    private const int DefaultObjectCount = 120;

    private int _activeCreateCoroutineCount = 0;

    // 오브젝트 풀이 생성될 때, 풀링 되는 객체들이 위치할 root 
    public ObjectPool()
    {
        GameObject go = GameObject.Find("@ObjectPool");

        if(go == null)
        {
            go = new GameObject { name = "@ObjectPool" };
        }

        _root = go.transform;
    }
    public Dictionary<string, List<PoolingObject>> PoolingObjects { get { return _poolingObjects; } }
    public int ActiveCreateCoroutineCount { get { return _activeCreateCoroutineCount; } }
    // 프레임당 생성량 반환
    private int WorkPerFrame { get { return Mathf.Max(MaxWorkPerFrame / _activeCreateCoroutineCount, 1); } }
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
        _poolingObjects.TryGetValue(key, out List<PoolingObject> objs);

        objs.Find(o => o.GameObject == prefab).isInUse = false;

        prefab.SetActive(false);
    }
    // 해당하는 키의 PoolingObject 반환, 활성화 여부 지정 가능
    public PoolingObject GetObject(string prefabKey, bool setInUse = true)
    {
        foreach(PoolingObject obj in _poolingObjects[prefabKey])
        {
            if(!obj.ActiveSelf && (!obj.isInUse || obj.isUsed))
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
    // 해당 키의 모든 PoolingObject 반환
    public List<PoolingObject> GetObjects(string prefabKey)
    {
        if(_poolingObjects.ContainsKey(prefabKey))
        {
            return _poolingObjects[prefabKey];
        }

        return null;
    }
    // 모든 PoolingObject를 초기 상태로 설정
    public void ReSetting()
    {
        foreach(List<PoolingObject> objList in _poolingObjects.Values)
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
    // 개수만큼 프리팹의 인스턴스 생성
    public void Create(GameObject prefab, int count = DefaultObjectCount)
    {
        CoroutineHelper.Start(CreatingInstance(prefab, count, false), CoroutineType.InGameSystem);
    }
    // 개수만큼 프리팹들의 인스턴스 생성
    public void Create(List<GameObject> prefabs, int count = DefaultObjectCount)
    {
        foreach(GameObject prefab in prefabs)
        {
            CoroutineHelper.Start(CreatingInstance(prefab, count), CoroutineType.InGameSystem);
        }
    }
    // 오브젝트 풀로 생성된 오든 오브젝트의 코루틴 중단
    public void StopAllActions()
    {
        foreach(List<PoolingObject> objs in _poolingObjects.Values)
        {
            foreach(PoolingObject obj in objs)
            {
                if(obj.ActiveSelf)
                {
                    obj.StopAllCoroutines();
                }
            }
        }
    }
    // 프리팹을 _root의 자식으로 개수만큼 생성
    private void CreateInstance(Transform _root, GameObject prefab, string key, int count, int instanceCount)
    {
        GameObject go;

        for(int i = 0; i < count; i++)
        {
            go = Object.Instantiate(prefab, _root);

            go.SetActive(false);
            _poolingObjects[key].Add(new(go));
        }
    }
    // 인스턴스가 위치할 root를 생성 및 poolingObjects에 등록 또는 할당
    private IEnumerator CreatingInstance(GameObject prefab, int count, bool isSetRoot = true)
    {
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

        _activeCreateCoroutineCount++;

        if (!_poolingObjects.ContainsKey(key))
        {
            _poolingObjects.Add(key, new());
        }

        if (isSetRoot)
        {
            transform.SetParent(_root);
        }

        while(instanceCount < count)
        {
            createCount = Mathf.Min(WorkPerFrame, count - instanceCount);

            CreateInstance(transform, prefab, key, createCount, instanceCount);

            instanceCount += createCount;

            yield return null;
        }

        _activeCreateCoroutineCount--;
    }
}
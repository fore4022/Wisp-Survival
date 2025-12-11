using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class ScriptableObject_Manage
{
    private Dictionary<string, ScriptableObject> scriptableObjects = new();

    private const int maxWorkPerFrame = 360;

    private int coroutineCount = 0;

    public int ScriptableObjectsCount { get { return scriptableObjects.Count; } }
    private int MaxWorkPerSec { get { return Mathf.Max(maxWorkPerFrame / coroutineCount, 1); } }
    // 키에 해당하는 ScriptableObject 반환
    public T GetScriptableObject<T>(string key) where T : ScriptableObject
    {
        if(scriptableObjects.ContainsKey(key))
        {
            return (T)scriptableObjects[key];
        }

        return null;
    }
    // 해당 키의 SO 불러오기, ScriptableObjectType에 따라서 ScriptableObject가 가지는 추가 오브젝트 생성 //
    public async Task LoadScriptableObject(ScriptableObjectType type, string key)
    {
        if(!scriptableObjects.ContainsKey(key))
        {
            ScriptableObject so = default;

            switch(type)
            {
                case ScriptableObjectType.Monster:
                    so = await AddressableHelper.LoadingToPath<ScriptableObject>($"Assets/SO/Monster/{Managers.Data.user.StageName}/{key}.asset");
                    break;
                case ScriptableObjectType.Skill:
                    so = await AddressableHelper.LoadingToPath<ScriptableObject>($"Assets/SO/Skill/{key}.asset");
                    break;
            }

            scriptableObjects.Add(key, so);
        }
    }
    // 입력 받은 배열의 모든 오브젝트에 키에 해당하는 ScriptableObject를 할당
    public IEnumerator SetScriptableObject(List<PoolingObject> list, string key)
    {
        ScriptableObject so;

        int sum = 0;
        int count;
        int index;

        coroutineCount++;

        so = scriptableObjects[key];

        while(sum < list.Count)
        {
            count = MaxWorkPerSec;

            for(index = sum; index < Mathf.Min(sum + count, list.Count); index++)
            {
                list[index].GetComponent<IScriptableData>().SO = so;
            }

            sum += count;

            yield return null;
        }

        coroutineCount--;
    }
}
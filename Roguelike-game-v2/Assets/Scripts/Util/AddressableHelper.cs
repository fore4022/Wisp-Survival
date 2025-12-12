using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
/// <summary>
/// <para>
/// Addressables 기반의 에셋 및 씬 로딩·해제 기능을 구현
/// </para>
/// 비동기 로딩과 메모리 관리 및 최적화
/// </summary>
public static class AddressableHelper
{
    private static List<AsyncOperationHandle> _handleList = new();
    private static Type _type_GameObject = typeof(GameObject);
    private static Type _type_ScriptableObject = typeof(ScriptableObject);
    private static Type _type_Sprite = typeof(Sprite);

    // 주소를 통해서 씬 로드
    public static void LoadingScene(string path)
    {
        Addressables.LoadSceneAsync(path, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
    // 주소를 통해서 에셋을 비동기 로드, 핸들의 저장 여부를 지정 가능
    public static async Task<T> LoadingToPath<T>(string path, bool releasable = true) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        await handle.Task;

        T result = handle.Result;

        if(releasable)
        {
            Type type = typeof(T);

            if(type == _type_GameObject || type == _type_ScriptableObject || type == _type_Sprite)
            {
                _handleList.Add(handle);
            }
            else
            {
                Addressables.Release(handle);
            }
        }

        return result;
    }
    // handleList에 등록된 모든 handle 해제
    public static void ResourcesRelease()
    {
        foreach(AsyncOperationHandle handle in _handleList)
        {
            Addressables.Release(handle);
        }

        _handleList.Clear();
    }
}
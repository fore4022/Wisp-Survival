using UnityEngine;
/// <summary>
/// 현재 씬에 맞춰서 카메라의 orthographic size scale 조정
/// </summary>
public class Camera_SizeScale : MonoBehaviour
{
    public static readonly float _deviceScale = (float)Screen.width / Screen.height;
    
    private const float DefaultScale = (float)1080 / 1920;

    private static bool _isDeviceScaleSmaller;

    private InGame_Camera _inGameCamera;

    public static float orthographicSizeScale { get { return _isDeviceScaleSmaller ? DefaultScale / _deviceScale : 1; } }
    private void Awake()
    {
        _inGameCamera = GetComponent<InGame_Camera>();

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Managers.Scene.loadComplete += OrthographicsSizeUpdate;

        _isDeviceScaleSmaller = _deviceScale <= DefaultScale;

#if UNITY_EDITOR
        _isDeviceScaleSmaller = false;
#endif

        OrthographicsSizeUpdate();
    }
    public void OrthographicsSizeUpdate()
    {
        switch(Managers.Scene.CurrentSceneName)
        {
            case "Title":
            case "Main":
                transform.position = new(0, 0, -10);
                _inGameCamera.enabled = false;

                ResizeOrthographicSize(CameraSizes.Common);
                break;
            case "InGame":
                if(!_inGameCamera.enabled)
                {
                    _inGameCamera.enabled = true;
                }

                ResizeOrthographicSize(CameraSizes.InGame);
                break;
        }
    }
    private void ResizeOrthographicSize(float size)
    {
        Camera.main.orthographicSize = size * orthographicSizeScale;
    }
}
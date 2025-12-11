using UnityEngine;
/// <summary>
/// 현재 씬에 맞춰서 카메라의 orthographic size scale 조정
/// </summary>
public class Camera_SizeScale : MonoBehaviour
{
    public static readonly float deviceScale = (float)Screen.width / Screen.height;
    
    private const float defaultScale = (float)1080 / 1920;

    private static bool isDeviceScaleSmaller;

    private InGame_Camera inGameCamera;

    public static float orthographicSizeScale { get { return isDeviceScaleSmaller ? defaultScale / deviceScale : 1; } }
    private void Awake()
    {
        inGameCamera = GetComponent<InGame_Camera>();

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Managers.Scene.loadComplete += OrthographicsSizeUpdate;

        isDeviceScaleSmaller = deviceScale <= defaultScale;

#if UNITY_EDITOR
        isDeviceScaleSmaller = false;
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
                inGameCamera.enabled = false;

                ResizeOrthographicSize(CameraSizes.Common);
                break;
            case "InGame":
                if(!inGameCamera.enabled)
                {
                    inGameCamera.enabled = true;
                }

                ResizeOrthographicSize(CameraSizes.inGame);
                break;
        }
    }
    private void ResizeOrthographicSize(float size)
    {
        Camera.main.orthographicSize = size * orthographicSizeScale;
    }
}
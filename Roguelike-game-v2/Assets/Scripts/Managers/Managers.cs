using UnityEngine;
/// <summary>
/// <para>
/// 주요 매니저를 담는 스크립트
/// </para>
/// 싱글톤으로 구현
/// </summary>
public class Managers : MonoBehaviour
{
    public static Managers managers;

    public new AudioManager audio = new();
    public GameManager game = new();
    public UIManager ui = new();
    public DataManager data = new();
    public SceneManager scene = new();
    public MainManager main = new();

    public static Managers Instance
    {
        get 
        { 
            Init();     

            return managers;
        }
    }
    public static AudioManager Audio { get { return Instance.audio; } }
    public static GameManager Game { get { return Instance.game; } }
    public static UIManager UI { get { return Instance.ui; } }
    public static DataManager Data { get { return Instance.data; } }
    public static SceneManager Scene { get { return Instance.scene; } }
    public static MainManager Main { get { return Instance.main; } }
    public static void Init()
    {
        if(managers == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null) 
            {
                go = new GameObject { name = "@Managers" };
            }

            if(!go.TryGetComponent(out managers))
            {
                managers = go.AddComponent<Managers>();
            }

            if(!go.GetComponent<ManagerInitializer>())
            {
                go.AddComponent<ManagerInitializer>();
            }

            DontDestroyOnLoad(go);
        }
    }
}
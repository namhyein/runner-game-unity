using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private static Managers Instance { get { Init(); return s_instance; } }

    private readonly APIManager _api = new();
    private readonly GameManager _game = new();
    private readonly HeartManager _heart = new();

    public static APIManager API { get { return Instance != null ? Instance._api : null; } }
    public static GameManager Game { get { return Instance != null ? Instance._game : null; } }
    public static HeartManager Heart { get { return Instance != null ? Instance._heart : null; } }

    #region Depends on UserManager
    private readonly CoinManager _coin = new();
    private readonly PotionManager _potion = new();
    private readonly DiamondManager _diamond = new();

    public static CoinManager Coin { get { return Instance != null ? Instance._coin : null; } }
    public static PotionManager Potion { get { return Instance != null ? Instance._potion : null; } }
    public static DiamondManager Diamond { get { return Instance != null ? Instance._diamond : null; } }
    #endregion

    #region Depends on Addressable
    private readonly MapManager _map = new();
    private readonly DataManager _data = new();
    private readonly AudioManager _audio = new();
    private readonly ObjectManager _object = new();
    private readonly LocalizationManager _localization = new();

    public static MapManager Map { get { return Instance != null ? Instance._map : null; } }
    public static DataManager Data { get { return Instance != null ? Instance._data : null; } }
    public static AudioManager Audio { get { return Instance != null ? Instance._audio : null; } }
    public static ObjectManager Object { get { return Instance != null ? Instance._object : null; } }
    public static LocalizationManager Localization { get { return Instance != null ? Instance._localization : null; } }
    #endregion

    #region Cores
    private readonly UIManager _ui = new();
    private readonly IAPManager _iap = new();
    private readonly UserManager _user = new();
    private readonly SceneManagerEx _scene = new();
    private readonly ResourceManager _resource = new();

    public static UIManager UI { get { return Instance != null ? Instance._ui : null; } }
    public static IAPManager IAP { get { return Instance != null ? Instance._iap : null; } }
    public static UserManager User { get { return Instance != null ? Instance._user : null; } }
    public static SceneManagerEx Scene { get { return Instance != null ? Instance._scene : null; } }
    public static ResourceManager Resource { get { return Instance != null ? Instance._resource : null; } }
    #endregion

    private void Start()
    {
        Application.targetFrameRate = 60;
        User.Init();
        Heart.Init();

        User.OnLoaded += InitializeOnUser;
    }

    private void InitializeOnUser(bool isLoaded)
    {
        if (!isLoaded) return;

        Coin.Init();
        Potion.Init();
        Diamond.Init();
    }

    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();
        }
    }

    public static void InitializeOnAddressable()
    {
        Data.Init();
        Map.Init();
        Audio.Init();
        Object.Init();
        Localization.Init();
    }
}

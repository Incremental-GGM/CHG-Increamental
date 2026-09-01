using UnityEngine;
using UnityEngine.Events;

public class GoldSystem : MonoBehaviour
{
    public static GoldSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GoldSystem>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<GoldSystem>();
                    obj.name = typeof(GoldSystem).Name;
                }
            }
            return _instance;
        }
    }
    private static GoldSystem _instance = null;
    
    public UnityEvent<int> OnGoldChanged;
    
    [SerializeField] private bool isDonDestroy = false;
    private int _gold;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as GoldSystem;
            if (isDonDestroy)
                DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void GetGold(int value)
    {
        _gold += value;
        OnGoldChanged.Invoke(_gold);
    }
}
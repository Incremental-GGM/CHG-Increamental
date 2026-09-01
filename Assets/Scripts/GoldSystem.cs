using UnityEngine;
using UnityEngine.Events;

public class GoldSystem : MonoBehaviour
{
    public UnityEvent<int> OnGoldChanged;
    
    private int _gold;
    [SerializeField] private int upperGoldValue;
    

    public void GetGold()
    {
        _gold += upperGoldValue;
        OnGoldChanged.Invoke(_gold);
    }
}
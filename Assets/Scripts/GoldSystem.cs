using UnityEngine;
using UnityEngine.Events;

public class GoldSystem : MonoBehaviour
{
    public UnityEvent<int> OnGoldChanged;
    
    private int _gold;
    

    public void GetGold(int value)
    {
        _gold += value;
        OnGoldChanged.Invoke(_gold);
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class GoldManager : MonoSingleton<GoldManager>
    {
    
        public UnityEvent<int> OnGoldChanged;
    
        [SerializeField] private bool isDonDestroy = false;
        private int _gold;
    
        private void Awake()
        {
            base.Awake();
        }

        public void GetGold(int value)
        {
            _gold += value;
            OnGoldChanged.Invoke(_gold);
        }
    }
}
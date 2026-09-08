using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class GoldManager : MonoSingleton<GoldManager>
    {
    
        public UnityEvent<int> OnGoldChanged;
    
        [SerializeField] private bool isDonDestroy = false;
        private int _gold;
        public int Gold => _gold;
    
        private void Awake()
        {
            base.Awake();
        }

        public bool TryBuy(int value)
        {
            if (Gold < value) return false;
            _gold -= value;
            OnGoldChanged.Invoke(_gold);
            return true;
        }
    }
}
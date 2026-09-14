using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class GoldManager : MonoSingleton<GoldManager>
    {
    
        public UnityEvent<BigNumber> OnGoldChanged;
    
        private BigNumber _gold = new BigNumber();
        public BigNumber Gold => _gold;
    
		public void AddGold(BigNumber amount)
		{
			if (amount.Mantissa <= 0)
				return;

			_gold += amount;


			OnGoldChanged?.Invoke(Gold);
		}

		public bool TryBuy(BigNumber value)
        {
            if (_gold < value)
                return false;
            _gold -= value;
            OnGoldChanged.Invoke(_gold);
            return true;
        }
    }
}
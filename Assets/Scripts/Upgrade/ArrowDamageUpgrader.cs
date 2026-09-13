using Manager;
using UnityEngine;

namespace Upgrade
{
    public class ArrowDamageUpgrader : MonoBehaviour
    {
        [SerializeField] private BigNumber _price;
        [SerializeField] private int _damage;
        [SerializeField] private int _priceUpperValue;

        [SerializeField] private Bow.Bow bow;
        
        
        public void HandleDamageUp()
        {
            if (!GoldManager.Instance.TryBuy(_price)) return;
            //bow.DamageUp(_damage);
        }
        
    }
}
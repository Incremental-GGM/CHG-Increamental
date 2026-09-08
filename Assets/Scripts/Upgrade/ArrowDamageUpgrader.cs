using Manager;
using UnityEngine;

namespace Upgrade
{
    public class ArrowDamageUpgrader : MonoBehaviour
    {
        [SerializeField] private int price;
        [SerializeField] private int damage;
        [SerializeField] private int priceUpperValue;

        [SerializeField] private Bow.Bow bow;
        
        
        public void HandleDamageUp()
        {
            if (!GoldManager.Instance.TryBuy(price)) return;
            bow.DamageUp(damage);
        }
        
    }
}
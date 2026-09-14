using _01.Scripts.CoreSystem.Manager;
using TMPro;
using UnityEngine;

namespace _01.Scripts.Upgrade
{
    public class UpgradeBtn : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI levelText;

        [SerializeField] private string Key;

        private UpgradeData _upgradeData;
        private void Start()
        {
             _upgradeData = UpgradeManager.Instance.GetUpgradeData(Key);
            titleText.text = _upgradeData.DisplayName;
            priceText.text = _upgradeData.InitialCost.ToString();
            levelText.text = _upgradeData.CurrentLevel.ToString();

            _upgradeData.OnStatChanged += HandleStatChange;
        }

        public void HandleBtnClick()
        {
            UpgradeManager.Instance.TryUpgrade(Key);
            Debug.Log("BtnClick");
        }
        
        private void HandleStatChange(UpgradeData data)
        {
            priceText.text = _upgradeData.InitialCost.ToString();
            levelText.text = _upgradeData.CurrentLevel.ToString();
        }
    }
}
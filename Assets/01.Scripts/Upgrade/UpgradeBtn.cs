using _01.Scripts.CoreSystem.Manager;
using _01.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _01.Scripts.Upgrade
{
    public class UpgradeBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI levelText;

        [SerializeField] private string Key;

        private UpgradeData _upgradeData;
        private bool _isHovered;

        private void Start()
        {
             _upgradeData = UpgradeManager.Instance.GetUpgradeData(Key);
            titleText.text = _upgradeData.DisplayName;
            priceText.text = $"{_upgradeData.InitialCost} Coin";
            levelText.text = _upgradeData.CurrentLevel.ToString();

            UpgradeManager.Instance.OnStatChanged += HandleStatChange;
        }

        private void OnDestroy()
        {
            if (UpgradeManager.Instance != null)
                UpgradeManager.Instance.OnStatChanged -= HandleStatChange;
        }

        public void HandleBtnClick()
        {
            UpgradeManager.Instance.TryUpgrade(Key);
            Debug.Log("BtnClick");
        }

        private void HandleStatChange(UpgradeData data)
        {
            if (data.Id != Key) return;

            _upgradeData = data;
            priceText.text = $"{_upgradeData.InitialCost} Coin";
            levelText.text = _upgradeData.CurrentLevel.ToString();

            if (_isHovered)
                InfoPanel.Instance.Show(_upgradeData, transform as RectTransform);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
            InfoPanel.Instance.Show(_upgradeData, transform as RectTransform);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
            InfoPanel.Instance.Hide();
        }
    }
}
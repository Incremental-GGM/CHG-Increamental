using System;
using Manager;
using TMPro;
using UnityEngine;

namespace Upgrade
{
    public class UpgradeBtn : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _priceText;

        [SerializeField] private string Key;
        [SerializeField] private string title;

        private void Start()
        {
            _titleText.text = title;
        }

        public void HandleBtnClick()
        {
            UpgradeManager.Instance.TryUpgrade(Key);
        }
    }
}
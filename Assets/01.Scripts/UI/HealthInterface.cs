using System;
using Manager;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthInterface : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;

        public void HandleHealthChange(int value)
        {
            healthText.text = value.ToString();
        }
    }
}
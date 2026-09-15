using _01.Scripts.Upgrade;
using TMPro;
using UnityEngine;

namespace _01.Scripts.UI
{
    public class InfoPanel : MonoSingleton<InfoPanel>
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private Vector2 offset = new Vector2(-250f, 0f);

        [SerializeField] private TextMeshProUGUI displayNameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI prerequisitesText;

        protected override void Awake()
        {
            base.Awake();
            panel.gameObject.SetActive(false);
        }

        public void Show(UpgradeData data, RectTransform target)
        {
            displayNameText.text = data.DisplayName;
            costText.text = $"Cost: {data.CalculateCost(data.CurrentLevel)}";
            valueText.text = $"Value: {data.InitialValue}";
            levelText.text = $"Level: {data.CurrentLevel}/{data.MaxLevel}";
            prerequisitesText.text = data.Prerequisites is { Count: > 0 }
                ? $"Prerequisites: {string.Join(", ", data.Prerequisites)}"
                : "Prerequisites: -";

            panel.position = target.position + (Vector3)offset;
            panel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            panel.gameObject.SetActive(false);
        }
    }
}
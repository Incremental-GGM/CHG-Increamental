using TMPro;
using UnityEngine;

namespace UI
{
    public class GoldInterface : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldText;

        public void GoldTextChange(BigNumber goldValue)
        {
            goldText.text = "Gold: " + goldValue;
        }
    }
}
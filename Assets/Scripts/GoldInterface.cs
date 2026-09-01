using TMPro;
using UnityEngine;

public class GoldInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    public void GoldTextChange(int goldValue)
    {
        goldText.text = "Gold: " + goldValue;
    }
}
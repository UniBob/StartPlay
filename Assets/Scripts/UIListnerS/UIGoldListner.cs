using TMPro;
using UnityEngine;

public class UIGoldListner : MonoBehaviour
{
    
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        GoldKeeperScript.goldUpdate += TextUpdate;
    }

    private void TextUpdate(int currentGold)
    {
        text.text = currentGold.ToString();
    }
}

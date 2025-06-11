using UnityEngine;
using TMPro;

public class PlayerGold : MonoBehaviour
{
    TextMeshProUGUI goldText;
    int gold = 0;

    private void Start()
    {
        goldText = GetComponent<TextMeshProUGUI>();
        goldText.text = gold.ToString();
    }

    public bool GoldChange(int amount)
    {
        if (gold + amount < 0)
            return false;

        gold += amount;
        goldText.text = gold.ToString();
        return true;
    }
}

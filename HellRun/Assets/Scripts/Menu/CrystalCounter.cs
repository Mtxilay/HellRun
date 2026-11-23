using UnityEngine;
using TMPro;

public class CrystalCounter : MonoBehaviour
{
    private TextMeshProUGUI crystalCounterText;
    void Awake()
    {
        crystalCounterText = GetComponent<TextMeshProUGUI>();
        int balance = Services.Save.GetInt("crystals");
        crystalCounterText.text = balance.ToString();
    }

}

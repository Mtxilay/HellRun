using UnityEngine;
using TMPro;
public class GuitarCard : MonoBehaviour
{
    public TextMeshProUGUI currentDamageText;
    public TextMeshProUGUI currentDurabilityText;

    public TextMeshProUGUI DurabilityUpgradeCost;

    public TextMeshProUGUI DamageUpgradeCost;

    public TextMeshProUGUI DurabilityUpgradeAmount;

    public TextMeshProUGUI DamageUpgradeAmount;

    public int guitarID;

    public TextMeshProUGUI titleText;
    public UnityEngine.UI.Image guitarImage;
    public UnityEngine.UI.Button damageUpgradeButton;
    public UnityEngine.UI.Button durabilityUpgradeButton;

    private GuitarData guitar;
    private GuitarCollectionManager collection;

    public void Initialize(GuitarData data, GuitarCollectionManager manager)
    {
        guitar = data;
        guitarID = data.guitarID;
        collection = manager;
        damageUpgradeButton.onClick.AddListener(UpgradeDamage);
        durabilityUpgradeButton.onClick.AddListener(UpgradeDurability);
        if (titleText != null)
            titleText.text = data.guitarName;
        if (guitarImage != null && data.guitarImage != null)
            guitarImage.sprite = data.guitarImage;
        Refresh();
    }

    public void Refresh()
    {
        GuitarProgression progression = MetaManager.GetGuitarProgression(guitarID);
        currentDamageText.text = $"current damage:{guitar.baseDamage + guitar.damagePerUpgrade * progression.damageLevel:0.##}";
        currentDurabilityText.text = $"current durability:{guitar.maxDurability + guitar.durabilityPerUpgrade * progression.durabilityLevel:0.##}";
        RefreshUpgrade(progression.damageLevel, guitar.damagePerUpgrade,
            DamageUpgradeCost, DamageUpgradeAmount, damageUpgradeButton, progression.unlocked);
        RefreshUpgrade(progression.durabilityLevel, guitar.durabilityPerUpgrade,
            DurabilityUpgradeCost, DurabilityUpgradeAmount, durabilityUpgradeButton, progression.unlocked);
    }

    private void RefreshUpgrade(int level, float amount, TextMeshProUGUI costText,
        TextMeshProUGUI amountText, UnityEngine.UI.Button button, bool unlocked)
    {
        bool maxed = level >= Services.Config.Economy.upgradeMaxLevel;
        int cost = maxed ? 0 : Services.Config.Economy.GetUpgradeCost(level + 1);
        costText.text = maxed ? "MAX" : $"cost :{cost} crystals";
        if (amountText != null)
            amountText.text = maxed ? "" : $"Upgrade bonus +{amount:0.##}";
        button.interactable = unlocked && !maxed && cost > 0 && Services.Save.GetInt("crystals") >= cost;
    }

    public void UpgradeDamage()
    {
        collection.UpgradeDamage(guitarID);
    }

    public void UpgradeDurability()
    {
        collection.UpgradeDurability(guitarID);
    }
}

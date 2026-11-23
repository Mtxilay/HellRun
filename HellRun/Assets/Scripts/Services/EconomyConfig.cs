
using UnityEngine;

[CreateAssetMenu(menuName = "HellRun/Economy Config")]
public class EconomyConfig : ScriptableObject
{
    [Header("Crystal Rewards")]
    public int crystalsPerMonster = 5;
    public int crystalsPerBoss = 80;

    [Header("Guitar Upgrades")]
    public int upgradeBaseCost = 100;
    public float upgradeGrowth = 1.35f;
    public int upgradeMaxLevel = 10;

    private int currentCost;

    public int GetUpgradeCost(int level)
    {
        if (level > upgradeMaxLevel)
        {
           return currentCost;
        }
        if (level < 1)
        {
           return upgradeBaseCost;
        }
        currentCost = Mathf.RoundToInt(upgradeBaseCost * Mathf.Pow(upgradeGrowth, level - 1));
        return currentCost;
    }
}

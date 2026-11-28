public static class MetaManager
{
    public static int currentGuitarID;

    public static void Initialize()
    {
        currentGuitarID = Services.Save.GetInt("CurrentGuitarID", 1);
        if (currentGuitarID <= 0)
        {
            currentGuitarID = 1;
        }
        Services.Save.SetInt("CurrentGuitarID", currentGuitarID);
        Services.Save.SetInt("Guitar1LockState", 1);
    }

    public static GuitarProgression GetGuitarProgression(int guitarID)
    {
        return new GuitarProgression
        {
            damageLevel = Services.Save.GetInt($"Guitar{guitarID}DamageLevel"),
            durabilityLevel = Services.Save.GetInt($"Guitar{guitarID}DurabilityLevel"),
            unlocked = Services.Save.GetInt($"Guitar{guitarID}LockState") == 1
        };
    }

    public static bool BuyGuitarDamageUpgrade(int guitarID)
    {
        return BuyUpgrade(guitarID, $"Guitar{guitarID}DamageLevel");
    }

    public static bool BuyGuitarDurabilityUpgrade(int guitarID)
    {
        return BuyUpgrade(guitarID, $"Guitar{guitarID}DurabilityLevel");
    }

    private static bool BuyUpgrade(int guitarID, string levelKey)
    {
        int level = Services.Save.GetInt(levelKey);
        if (guitarID <= 0 || level < 0 ||
            Services.Save.GetInt($"Guitar{guitarID}LockState") != 1 ||
            level >= Services.Config.Economy.upgradeMaxLevel)
            return false;

        int cost = Services.Config.Economy.GetUpgradeCost(level + 1);
        int balance = Services.Save.GetInt("crystals");
        if (cost <= 0 || balance < cost)
            return false;

        Services.Save.SetInt("crystals", balance - cost);
        Services.Save.SetInt(levelKey, level + 1);
        return true;
    }
}

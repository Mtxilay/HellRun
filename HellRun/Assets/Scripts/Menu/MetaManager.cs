using System.ComponentModel.Design;
using System.Runtime.Serialization;
using System.Text.Json.Nodes;
using UnityEngine;

public class MetaManager
{

 public static int currentGuitarID; 

 private GuitarData equippedGuitar;

 public static void Initialize()
    {
        currentGuitarID = Services.Save.GetInt("CurrentGuitarID");
        if(currentGuitarID==0)
        {
            currentGuitarID=1;
            Services.Save.SetInt($"Guitar{currentGuitarID}LockState",1);
        }
    }  


public static GuitarProgression GetGuitarProgression(int guitarID)
    {
        GuitarProgression guitarProgression = new GuitarProgression();
        guitarProgression.damageLevel=Services.Save.GetInt($"Guitar{guitarID}DamageLevel");
        guitarProgression.durabilityLevel=Services.Save.GetInt($"Guitar{guitarID}DurabilityLevel");
        guitarProgression.unlocked= Services.Save.GetInt($"Guitar{guitarID}LockState")==0 ? false: true;
        return guitarProgression;

    }


public static bool BuyGuitarDamageUpgrade(int guitarID)
    {
        
        int balance = Services.Save.GetInt("crystals");
        int damagelvl=Services.Save.GetInt($"Guitar{guitarID}DamageLevel");
        int cost = Services.Config.Economy. GetUpgradeCost(damagelvl);
        bool unlocked = Services.Save.GetInt($"Guitar{guitarID}LockState")==0 ? false: true;
        if (damagelvl >= Services.Config.Economy.upgradeMaxLevel || balance<cost || unlocked==false) return false;
        Services.Save.SetInt($"Guitar{guitarID}DamageLevel",damagelvl+1);
        Services.Save.SetInt("crystals",balance-cost);
        return true;
    }

public static void BuyGuitarDurabilityUpgrade(int cost,int guitarID)
    {
        
    }

}

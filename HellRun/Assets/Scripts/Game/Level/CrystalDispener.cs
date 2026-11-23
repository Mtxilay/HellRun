using UnityEngine;

public class CrystalDispener : MonoBehaviour
{

    public void DropBasicCrystal()
    {
        GameObject crystalPrefab = Resources.Load<GameObject>("Prefabs/Crystals/BasicCrystal");
        GameObject crystal = Instantiate(crystalPrefab, transform.position, Quaternion.identity, transform.parent);
        int value = Services.Config.Economy.crystalsPerMonster;
        crystal.GetComponent<CrystalPickup>().Initialize(value);
    }
}


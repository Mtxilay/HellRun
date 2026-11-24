
using System.Collections.Generic;
using UnityEngine;

public class GuitarCollectionManager : MonoBehaviour
{
    private List<GuitarProgression> guitarProgressions;

    private List<GameObject> GuitarCards;

    [SerializeField] private GameObject collectionHolder;

    public int NumOfGuitars;
    void Start()
    {
        guitarProgressions=new List<GuitarProgression>();
        for(int guitarID=1;guitarID<=NumOfGuitars;guitarID++)
        {
            if(Services.Save.GetInt($"Guitar{guitarID}LockState")==1)
                guitarProgressions.Add(MetaManager.GetGuitarProgression(guitarID));
        }

    }

    public void SetCollectionDisplayData()
    {
        foreach(var card in GuitarCards)
        {
            
        }
    }


    public void UpgradeDamage(int guitarID)
    {
        MetaManager.BuyGuitarDamageUpgrade(guitarID);
    }
}

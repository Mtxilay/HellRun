
using System.Collections.Generic;
using UnityEngine;

public class GuitarCollectionManager : MonoBehaviour
{
    private readonly List<GuitarCard> guitarCards = new List<GuitarCard>();

    [SerializeField] private GameObject collectionHolder;

    [SerializeField] private GuitarCard cardPrefab;
    [SerializeField] private List<GuitarData> guitars;
    [SerializeField] private TMPro.TextMeshProUGUI crystalCounterText;
    void Start()
    {
        MetaManager.Initialize();
        foreach (GuitarData guitar in guitars)
        {
            if (!MetaManager.GetGuitarProgression(guitar.guitarID).unlocked)
                continue;
            GuitarCard card = Instantiate(cardPrefab, collectionHolder.transform, false);
            card.Initialize(guitar, this);
            guitarCards.Add(card);
        }
        SetCollectionDisplayData();
    }

    public void SetCollectionDisplayData()
    {
        foreach (GuitarCard card in guitarCards)
            card.Refresh();
        if (crystalCounterText != null)
            crystalCounterText.text = Services.Save.GetInt("crystals").ToString();
    }


    public void UpgradeDamage(int guitarID)
    {
        MetaManager.BuyGuitarDamageUpgrade(guitarID);
        SetCollectionDisplayData();
    }

    public void UpgradeDurability(int guitarID)
    {
        MetaManager.BuyGuitarDurabilityUpgrade(guitarID);
        SetCollectionDisplayData();
    }

    private void OnEnable()
    {
        if (guitarCards.Count > 0)
            SetCollectionDisplayData();
    }
}

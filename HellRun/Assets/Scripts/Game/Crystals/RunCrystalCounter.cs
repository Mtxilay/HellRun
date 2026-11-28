using TMPro;
using UnityEngine;

public class RunCrystalCounter : MonoBehaviour
{
    private TextMeshProUGUI counterText;
    private PlayerGuitar playerGuitar;
    private bool saved;

    private void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
        Application.targetFrameRate = 30;
        CrystalPickup.ResetRun();
        counterText.text = "0";

        CrystalPickup.OnCrystalsCollected += UpdateCounter;
        playerGuitar = FindAnyObjectByType<PlayerGuitar>();
        playerGuitar.OnDurabilityDamage += SaveOnDeath;
    }

    private void UpdateCounter(int collected)
    {
        counterText.text = collected.ToString("N0");
    }

    private void SaveOnDeath(float durability)
    {
        if (durability > 0 || saved)
            return;

        saved = true;
        int balance = Services.Save.GetInt("crystals");
        Services.Save.SetInt("crystals", balance + CrystalPickup.CollectedThisRun);
    }

    private void OnDestroy()
    {
        CrystalPickup.OnCrystalsCollected -= UpdateCounter;
        if (playerGuitar != null)
            playerGuitar.OnDurabilityDamage -= SaveOnDeath;
    }
}

using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    private int crystalValue;
    public void Initialize(int value)
    {
        crystalValue = value;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerGuitar playerGuitar = other.GetComponentInParent<PlayerGuitar>();

        if (playerGuitar == null)
            return;

        int balance = Services.Save.GetInt("crystals");
        Services.Save.SetInt("crystals", balance + crystalValue);
        Debug.Log($"Crystals: {balance + crystalValue}");
        Destroy(gameObject);
    }
}

using System;
using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    public static int CollectedThisRun { get; private set; }
    public static event Action<int> OnCrystalsCollected;

    private int crystalValue;
    private bool collected;

    public static void ResetRun()
    {
        CollectedThisRun = 0;
    }

    public void Initialize(int value)
    {
        crystalValue = value;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerGuitar playerGuitar = other.GetComponentInParent<PlayerGuitar>();

        if (playerGuitar == null || collected)
            return;

        collected = true;
        CollectedThisRun += crystalValue;
        OnCrystalsCollected?.Invoke(CollectedThisRun);
        Destroy(gameObject);
    }
}

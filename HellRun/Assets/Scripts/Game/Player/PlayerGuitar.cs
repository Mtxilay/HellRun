using System;
using UnityEngine;

public class PlayerGuitar : MonoBehaviour
{
    [SerializeField] private GuitarData equippedGuitar;
    public event Action<float> OnDurabilityDamage;

    public float CurrentDurability { get; private set; }

    public float MaxDurability { get; private set; }

    public float Damage => equippedGuitar.baseDamage;

    private void Awake()
    {
        CurrentDurability = equippedGuitar.maxDurability;
        MaxDurability = equippedGuitar.maxDurability;
    }

    public void TakeDamage(float damage)
    {
        CurrentDurability = Mathf.Max(0, CurrentDurability - damage);
        OnDurabilityDamage?.Invoke(CurrentDurability);

        if (CurrentDurability <= 0)
        {
            this.GetComponentInParent<LevelManager>().EndRun();
        }
    }



}
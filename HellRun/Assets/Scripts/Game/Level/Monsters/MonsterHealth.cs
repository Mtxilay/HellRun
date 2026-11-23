using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;

    private float chordMultiplier = 1;

    private float rythemMultiplier = 1;
    [SerializeField] private ChordType chordType;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void onDamage(float damage, ChordType chordType)
    {
        CalculateMultipliers(chordType);
        currentHealth -= damage * chordMultiplier * rythemMultiplier;
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        this.gameObject.GetComponent<CrystalDispener>().DropBasicCrystal();
        Destroy(this.gameObject);
    }

    void CalculateMultipliers(ChordType chordType)
    {
        if (chordType == this.chordType)
        {
            chordMultiplier = 1f;
        }
        else
        {
            chordMultiplier = 0.5f;
        }

        if (CheckRhythm())
        {
            rythemMultiplier = 1f;
        }
        else
        {
            rythemMultiplier = 0.5f;
        }
    }

    private bool CheckRhythm()
    {
        // Implement rhythm checking logic here
        return true; // Placeholder
    }
}

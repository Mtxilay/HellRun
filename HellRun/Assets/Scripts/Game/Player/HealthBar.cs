using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{

   [SerializeField] private PlayerGuitar playerGuitar;
    [SerializeField] private Slider healthSlider;

    void OnEnable()
    {
        playerGuitar.OnDurabilityDamage += UpdateHealthBar;
    }

       void OnDisable()
    {
        playerGuitar.OnDurabilityDamage -= UpdateHealthBar;
    }


    public void UpdateHealthBar(float currentDurability)
    {
        float healthPercentage = currentDurability / playerGuitar.MaxDurability;
        healthSlider.value = healthPercentage;
    }

}

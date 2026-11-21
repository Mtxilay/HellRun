
using UnityEngine;


public class MonsterAttacker : MonoBehaviour
{
    private float attackDamage = 10f;


    private void OnTriggerEnter2D(Collider2D other)
{
    PlayerGuitar playerGuitar = other.GetComponentInParent<PlayerGuitar>();

    if (playerGuitar == null)
        return;

    playerGuitar.TakeDamage(attackDamage);
    Destroy(gameObject);
}
    
}

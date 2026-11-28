using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private NormalAttack normalAttack;
    private PlayerGuitar playerGuitar;
    [SerializeField] private float attackCooldown = 0.3f;
    private float nextAttackTime;


    void Awake()
    {
        playerGuitar = this.gameObject.GetComponentInParent<PlayerGuitar>();
    }


    public void NormalAttack(string chordString)
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + attackCooldown;

        ChordType chordType = (ChordType)System.Enum.Parse(typeof(ChordType), chordString);
        normalAttack = Instantiate(Resources.Load<NormalAttack>("Prefabs/Attacks/NormalAttack"), this.transform.position, Quaternion.identity);
        normalAttack.Initialize(playerGuitar.Damage, chordType);
    }
}

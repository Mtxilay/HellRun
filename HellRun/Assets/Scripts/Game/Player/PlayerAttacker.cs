using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private NormalAttack normalAttack;
    private PlayerGuitar playerGuitar;


    void Awake()
    {
        playerGuitar = this.gameObject.GetComponentInParent<PlayerGuitar>();
    }


    public void NormalAttack(string chordString)
    {
        ChordType chordType = (ChordType)System.Enum.Parse(typeof(ChordType), chordString);
        normalAttack = Instantiate(Resources.Load<NormalAttack>("Prefabs/Attacks/NormalAttack"), this.transform.position, Quaternion.identity);
        normalAttack.Initialize(playerGuitar.Damage, chordType);
    }
}

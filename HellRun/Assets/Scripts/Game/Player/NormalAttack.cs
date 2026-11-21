
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float Speed = 20f;
    [SerializeField, Min(0.1f)] private float maxLength = 15f;

    [SerializeField] private Sprite spriteD;
    [SerializeField] private Sprite spriteE;
    [SerializeField] private Sprite spriteF;
    [SerializeField] private Sprite spriteG;    

    private Transform laser;
    private Vector3 origin;

    private float damage;
    private ChordType chord;

    private void Awake()
    {
        laser = this.gameObject.GetComponent<Transform>();
        if (laser == null)
        {
            Debug.LogError("NormalAttack needs a child SpriteRenderer.", this);
            enabled = false;
            return;
        }

        origin = transform.position;
        laser.transform.position = origin;
    }

    public void Initialize(float damage, ChordType chord)
    {
        this.damage = damage;
        this.chord = chord;
        this.GetComponentInChildren<SpriteRenderer>().sprite = GetSpriteForChord(chord);
    }

    private void Update()
    {
         AdvanceProjectile();
    }

    private void AdvanceProjectile()
    {
        bool finished = laser.transform.position.y >= origin.y + maxLength;

        // RaycastAll returns nearest hits first; ignore non-monster colliders.
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(origin, Vector2.up, maxLength))
        {
            MonsterHealth monster = hit.collider.GetComponentInParent<MonsterHealth>();
            if (monster == null)
                continue;
            if (laser.transform.position.y <= hit.point.y)
                break;
            monster.onDamage(damage, chord);
            finished = true;
            break;
        }

        laser.transform.position +=Time.deltaTime * Vector3.up * Speed; 

        if(finished)
        {
            enabled = false;
            Destroy(gameObject, 0.1f);
        }
    }

    private Sprite GetSpriteForChord(ChordType chord)
    {

        switch (chord)
        {
            case ChordType.D:
                return spriteD;
            case ChordType.E:
                return spriteE;
            case ChordType.F:
                return spriteF;
            case ChordType.G:
                return spriteG;
            default:
                Debug.LogWarning("Unknown chord type: " + chord);
                return null;
        }
    }
}

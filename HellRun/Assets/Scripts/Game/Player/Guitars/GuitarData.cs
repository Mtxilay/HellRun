using UnityEngine;

[CreateAssetMenu(fileName = "GuitarData", menuName = "Hell Run/GuitarData")]
public class GuitarData : ScriptableObject
{
    public string guitarName;
    public int guitarID;
    public Sprite guitarImage;
    public float baseDamage;
    public float maxDurability;

    public float damagePerUpgrade;
    public float durabilityPerUpgrade;

}

using UnityEngine;

[CreateAssetMenu(fileName = "GuitarData", menuName = "Scriptable Objects/GuitarData")]
public class GuitarData : ScriptableObject
{
    public string guitarName;
    public int guitarID;
    public Sprite guitarImage;
    public float baseDamage;
    public float maxDurability;

}

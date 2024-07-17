using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Item/Weapon")]
public class WeaponItem : ItemBase
{
    public int Damage;
    public int Range;
    public float AttacksPerSecond;
}

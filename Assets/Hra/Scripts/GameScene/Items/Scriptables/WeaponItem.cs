using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Item/Weapon")]
public class WeaponItem : ItemBase
{
    public int Damage;
    public int Range;
    public float AttacksPerSecond;

    public float GetValueFromStat(WeaponStat weaponStat)
    {
        return weaponStat switch
        {
            WeaponStat.Damage => Damage,
            WeaponStat.Range => Range,
            WeaponStat.AttackRate => AttacksPerSecond,
            _ => 0,
        };
    }
}

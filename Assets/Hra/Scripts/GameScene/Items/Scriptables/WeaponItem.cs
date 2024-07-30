using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Item/Weapon")]
public class WeaponItem : ItemBase
{
    public List<float> Damage;
    public List<int> Range;
    public List<float> AttacksPerSecond;

    public float GetValueFromStat(WeaponStat weaponStat)
    {
        int level = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(item => item.FriendlyID == FriendlyID).Level;
        return weaponStat switch
        {
            WeaponStat.Damage => Damage[level - 1],
            WeaponStat.Range => Range[level - 1],
            WeaponStat.AttackRate => AttacksPerSecond[level - 1],
            _ => 0,
        };
    }
}


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipSpawner
{
    private Dictionary<WeaponStat, string> _weaponStatToolTips = new()
    {
        { WeaponStat.Damage, "Damage" },
        { WeaponStat.Range, "Range" },
        { WeaponStat.AttackRate, "Attack Rate" },
        { WeaponStat.EnemySlow, "Enemy Slow" },
        { WeaponStat.Dot, "DoT" },
        { WeaponStat.Heal, "Heal" },
        { WeaponStat.ChainDamage, "Chain Damage" },
    };

    public string SetToolTip(RaycastResult raycast)
    {
        if (raycast.gameObject.TryGetComponent(out LoadoutStat stat))
        {
            return HandleStatToolTip(stat);
        }

        return null;
    }

    private string HandleStatToolTip(LoadoutStat stat)
    {
        return _weaponStatToolTips[stat.WeaponStat];
    }
}

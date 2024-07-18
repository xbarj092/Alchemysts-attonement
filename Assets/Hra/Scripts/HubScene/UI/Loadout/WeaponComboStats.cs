using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponComboStats : MonoBehaviour
{
    private Dictionary<string, Dictionary<WeaponStat, float>> _comboStats = new()
    {
        { "Scorching-Zappy", new(){ { WeaponStat.Damage, -10 } } },
        { "Scorching-Arctic", new(){ { WeaponStat.Damage, -10 } } },
        { "Scorching-Life Sucking", new(){ { WeaponStat.Damage, -10 } } },
        { "Arctic-Zappy", new(){ { WeaponStat.Damage, -10 } } },
        { "Life Sucking-Arctic", new(){ { WeaponStat.Damage, -10 } } },
        { "Life Sucking-Zappy", new(){ { WeaponStat.Damage, -10 } } },
        { "Scorching-Arctic-Zappy", new(){ { WeaponStat.Damage, -10 } } },
        { "Scorching-Life Sucking-Arctic", new(){ { WeaponStat.Damage, -10 } } },
        { "Scorching-Life Sucking-Zappy", new(){ { WeaponStat.Damage, -10 } } },
        { "Life Sucking-Arctic-Zappy", new(){ { WeaponStat.Damage, -10 } } },
        { "Scorching-Life Sucking-Arctic-Zappy", new(){ { WeaponStat.Damage, -10 } } }
    };

    public Dictionary<WeaponStat, float> GetComboStat()
    {
        List<ElementItem> equippedElements = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements;
        if (equippedElements == null || equippedElements.Count == 0)
        {
            return null;
        }

        equippedElements.Sort((x, y) => x.AdjectivePosition.CompareTo(y.AdjectivePosition));
        string key = string.Join("-", equippedElements.Select(e => e.Adjective).ToArray());
        if (_comboStats.TryGetValue(key, out Dictionary<WeaponStat, float> comboStat))
        {
            return comboStat;
        }

        return null;
    }
}

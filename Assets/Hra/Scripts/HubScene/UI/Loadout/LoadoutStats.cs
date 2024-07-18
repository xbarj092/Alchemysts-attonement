using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadoutStats : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<SpecialEffect, WeaponStat> _statDefinitions;
    [SerializeField] private List<LoadoutStat> _loadoutStats = new();

    private List<LoadoutStat> _updatedStats = new();

    public void UpdateStats()
    {
        _updatedStats.Clear();
        foreach (LoadoutStat stat in _loadoutStats)
        {
            stat.gameObject.SetActive(true);
            stat.Init(0);
        }

        HandleWeaponStats();

        foreach (ElementItem equippedElement in LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements)
        {
            HandleElementStats(equippedElement);
        }

        DisableUnusedStats();
    }

    private void HandleElementStats(ElementItem equippedElement)
    {
        foreach (KeyValuePair<SpecialEffect, List<float>> specialEffect in equippedElement.SpecialEffects)
        {
            float value = specialEffect.Value[LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == equippedElement.FriendlyID).Level];
            SetUpStat(_statDefinitions[specialEffect.Key], value, true);
        }
    }

    private void HandleWeaponStats()
    {
        WeaponItem equippedWeapon = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedWeapon;
        if (equippedWeapon == null)
        {
            return;
        }

        SetUpStat(WeaponStat.Damage, equippedWeapon.Damage);
        SetUpStat(WeaponStat.Range, equippedWeapon.Range);
        SetUpStat(WeaponStat.AttackRate, equippedWeapon.AttacksPerSecond);
    }
    
    private void SetUpStat(WeaponStat weaponStat, float value, bool special = false)
    {
        LoadoutStat loadoutStat = _loadoutStats.FirstOrDefault(stat => stat.WeaponStat == weaponStat);
        if (!_updatedStats.Contains(loadoutStat))
        {
            _updatedStats.Add(loadoutStat);
        }

        loadoutStat.Init(value, special);
    }

    private void DisableUnusedStats()
    {
        foreach (LoadoutStat loadoutStat in _loadoutStats)
        {
            if (!_updatedStats.Contains(loadoutStat))
            {
                loadoutStat.gameObject.SetActive(false);
            }
        }
    }
}

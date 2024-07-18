using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadoutStats : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<SpecialEffect, WeaponStat> _statDefinitions;
    [SerializeField] private List<LoadoutStat> _loadoutStats = new();

    private List<LoadoutStat> _updatedStats = new();

    private void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        _updatedStats.Clear();
        foreach (LoadoutStat stat in _loadoutStats)
        {
            stat.gameObject.SetActive(true);
            stat.Init(0, init: true);
        }

        HandleWeaponStats();

        foreach (ElementItem equippedElement in LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements)
        {
            HandleElementStats(equippedElement);
        }

        DisableUnusedStats();
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

    private void HandleElementStats(ElementItem equippedElement)
    {
        foreach (KeyValuePair<SpecialEffect, List<float>> specialEffect in equippedElement.SpecialEffects)
        {
            float value = specialEffect.Value[LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == equippedElement.FriendlyID).Level - 1];
            SetUpStat(_statDefinitions[specialEffect.Key], value, true);
        }
    }

    private void SetUpStat(WeaponStat weaponStat, float value, bool special = false)
    {
        LoadoutStat loadoutStat = _loadoutStats.FirstOrDefault(stat => stat.WeaponStat == weaponStat);
        if (!_updatedStats.Contains(loadoutStat))
        {
            _updatedStats.Add(loadoutStat);
        }
        else
        {
            LoadoutStat existingStat = _updatedStats.FirstOrDefault(stat => stat == loadoutStat);
            if (existingStat != null)
            {
                existingStat.Init(value, special);
                return;
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutWeapon : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemName;

    private List<WeaponItem> _boughtWeapons = new();
    private WeaponItem _currentWeapon;
    private int _currentWeaponIndex = 0;

    public event Action<WeaponItem> OnWeaponItemChanged;

    private void Awake()
    {
        GetBoughtWeapons();
    }

    private void GetBoughtWeapons()
    {
        List<string> friendlyIDs = new();
        List<UpgradeData> upgradeData = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.Where(upgrade => upgrade.ItemType == ItemType.Weapon).ToList();
        if (upgradeData == null || upgradeData.Count == 0)
        {
            return;
        }

        foreach (UpgradeData upgrade in upgradeData)
        {
            friendlyIDs.Add(upgrade.FriendlyID);
        }

        foreach (ItemBase catalogItem in LocalDataStorage.Instance.Catalog.CatalogItemList)
        {
            if (friendlyIDs.Contains(catalogItem.FriendlyID))
            {
                _boughtWeapons.Add(catalogItem as WeaponItem);
            }
        }

        SelectWeapon(true);
    }

    public void SelectNext()
    {
        _currentWeaponIndex++;
        if (_currentWeaponIndex >= _boughtWeapons.Count)
        {
            _currentWeaponIndex = 0;
        }

        SelectWeapon();
    }

    public void SelectPrevious()
    {
        _currentWeaponIndex--;
        if (_currentWeaponIndex < 0)
        {
            _currentWeaponIndex = _boughtWeapons.Count - 1;
        }

        SelectWeapon();
    }

    private void SelectWeapon(bool init = false)
    {
        if (_boughtWeapons.Count == 0)
        {
            return;
        }

        if (init)
        {
            for (int i = 0; i < _boughtWeapons.Count; i++)
            {
                if (LocalDataStorage.Instance.PlayerData.LoadoutData?.EquippedWeapon?.FriendlyID == _boughtWeapons[i].FriendlyID)
                {
                    _currentWeaponIndex = i;
                }
            }

            _currentWeapon = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedWeapon == null ?
                _boughtWeapons[_currentWeaponIndex] : LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedWeapon;
        }
        else
        {
            _currentWeapon = _boughtWeapons[_currentWeaponIndex];
        }

        _itemImage.sprite = _currentWeapon.Icon;
        _itemName.text = _currentWeapon.Name;

        LocalDataStorage.Instance.PlayerData.LoadoutData = new(LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements, _currentWeapon);
        OnWeaponItemChanged?.Invoke(_currentWeapon);
    }
}

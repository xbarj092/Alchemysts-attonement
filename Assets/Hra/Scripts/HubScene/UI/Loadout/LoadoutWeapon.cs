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
    [SerializeField] private TMP_Text _itemDescription;

    private List<WeaponItem> _boughtWeapons = new();
    private WeaponItem _currentWeapon;
    private int _currentWeaponIndex = 0;

    private WeaponAdjectives _weaponAdjectives = new();
    private WeaponDescription _weaponDescription = new();

    public event Action<WeaponItem> OnWeaponItemChanged;

    private void Awake()
    {
        GetBoughtWeapons();
    }

    public void ChangeWeaponText()
    {
        ChangeWeaponName();
        ChangeWeaponDescription();
    }

    private void ChangeWeaponName()
    {
        string adjectives = _weaponAdjectives.GetWeaponAdjectives();
        string fullName = adjectives == null ? _currentWeapon.Name : adjectives + " " + _currentWeapon.Name;
        _itemName.text = fullName;
    }

    private void ChangeWeaponDescription()
    {
        string description = _weaponDescription.GetWeaponDescription();
        _itemDescription.text = description;
    }

    private void GetBoughtWeapons()
    {
        List<string> friendlyIDs = new();
        List<UpgradeData> upgradeData = LocalDataStorage.Instance.PlayerData.UpgradesData.
            UpgradeData.Where(upgrade => upgrade.ItemType == ItemType.Weapon).ToList();
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

        LoadoutData loadoutData = LocalDataStorage.Instance.PlayerData.LoadoutData;

        if (init)
        {
            for (int i = 0; i < _boughtWeapons.Count; i++)
            {
                if (loadoutData?.EquippedWeapon?.FriendlyID == _boughtWeapons[i].FriendlyID)
                {
                    _currentWeaponIndex = i;
                }
            }

            _currentWeapon = loadoutData.EquippedWeapon == null ? 
                _boughtWeapons[_currentWeaponIndex] : loadoutData.EquippedWeapon;
        }
        else
        {
            _currentWeapon = _boughtWeapons[_currentWeaponIndex];
        }

        _itemImage.sprite = _currentWeapon.Icon;
        ChangeWeaponText();

        LocalDataStorage.Instance.PlayerData.LoadoutData = new(loadoutData.EquippedElements, 
            _currentWeapon, loadoutData.WeaponInstance);
        OnWeaponItemChanged?.Invoke(_currentWeapon);
    }
}

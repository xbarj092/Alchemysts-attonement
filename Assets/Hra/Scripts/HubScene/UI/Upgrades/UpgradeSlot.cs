﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{ 
    [Header("Image")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private Sprite _itemImageSprite;
    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private Sprite _fullyUpgradedSprite;

    [Header("stuff")]
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private string _friendlyId;
    [SerializeField] private List<Image> _levels = new();
    [SerializeField] private TMP_Text _priceText;

    private UpgradeData _upgradeData;
    private ItemBase _catalogItem;

    

    private void Awake()
    {
        _itemImage.sprite = _itemImageSprite;
        GetUpgradeData();
        InitSlot();
    }

    private void GetUpgradeData()
    {
        _upgradeData = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == _friendlyId);
        _catalogItem = LocalDataStorage.Instance.Catalog.CatalogItemList.FirstOrDefault(item => item.FriendlyID == _friendlyId);
    }

    private void InitSlot()
    {
        _itemName.text = _catalogItem.Name;

        if (_upgradeData == null)
        {
            _itemImage.sprite = _lockedSprite;
            foreach (Image level in _levels)
            {
                level.gameObject.SetActive(false);
            }

            _priceText.text = _catalogItem.UpgradePrices[0].ToString();
            return;
        }
        else if (_upgradeData.Level == _upgradeData.MaxLevel)
        {
            _itemImage.sprite = _fullyUpgradedSprite;
            _priceText.text = "MAX";
        }
        else
        {
            //_itemImage.sprite = _catalogItem.Icon;    //To pod tím funguje, kdyžtak to přepiš jinak, idk
            _itemImage.sprite = _itemImageSprite;
            _priceText.text = _catalogItem.UpgradePrices[_upgradeData.Level].ToString();
        }

        for (int i = 0; i < _levels.Count; i++)
        {
            _levels[i].gameObject.SetActive(i < _upgradeData.Level);
        }
    }
     
    public void TryPurchase()
    {
        if (_upgradeData?.Level >= _upgradeData?.MaxLevel)
        {
            return;
        }

        UpgradesData upgradesData = LocalDataStorage.Instance.PlayerData.UpgradesData;
        CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;

        if (_upgradeData == null && currencyData.Coins >= _catalogItem.UpgradePrices[0])
        {
            currencyData.Coins -= _catalogItem.UpgradePrices[0];
            upgradesData.UpgradeData.Add(new(_catalogItem.ItemType, _friendlyId, true, 1, _catalogItem.MaxLevel));
        }
        else if (_upgradeData != null && (_upgradeData.Level != _upgradeData.MaxLevel || currencyData.Coins >= _catalogItem.UpgradePrices[_upgradeData.Level]))
        {
            currencyData.Coins -= _catalogItem.UpgradePrices[_upgradeData.Level];
            upgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == _friendlyId).Level++;
        }

        LocalDataStorage.Instance.PlayerData.UpgradesData = upgradesData;
        LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;

        GetUpgradeData();
        InitSlot();
    }
}

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{ 
    [Header("Image")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private Sprite _fullyUpgradedSprite;

    [Header("stuff")]
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private string _friendlyId;
    [SerializeField] private List<Image> _levels = new();

    private UpgradeData _upgradeData;
    private ItemBase _catalogItem;

    private void Awake()
    {
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

            return;
        }
        else if (_upgradeData.Level == 5)
        {
            _itemImage.sprite = _fullyUpgradedSprite;
        }
        else
        {
            _itemImage.sprite = _catalogItem.Icon;
        }

        for (int i = 0; i < _levels.Count; i++)
        {
            _levels[i].gameObject.SetActive(i < _upgradeData.Level);
        }
    }

    public void TryPurchase()
    {
        if (_upgradeData == null && LocalDataStorage.Instance.PlayerData.CurrencyData.Coins >= _catalogItem.UpgradePrices[0])
        {
            LocalDataStorage.Instance.PlayerData.CurrencyData.Coins -= _catalogItem.UpgradePrices[0];
            LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.Add(new(_catalogItem.ItemType, _friendlyId, true, 1));
        }
        else if (_upgradeData != null && (_upgradeData.Level != 5 || LocalDataStorage.Instance.PlayerData.CurrencyData.Coins >= _catalogItem.UpgradePrices[_upgradeData.Level]))
        {
            LocalDataStorage.Instance.PlayerData.CurrencyData.Coins -= _catalogItem.UpgradePrices[_upgradeData.Level];
            LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == _friendlyId).Level++;
        }

        GetUpgradeData();
        InitSlot();
    }
}

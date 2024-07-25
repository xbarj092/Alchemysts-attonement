using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutElement : MonoBehaviour
{
    public Button Button;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    public ElementItem ElementItem;

    public event Action<bool> OnElementsChanged;

    public void Init(ElementItem elementItem)
    {
        ElementItem = elementItem;

        _image.preserveAspect = true;
        _image.sprite = elementItem.Icon;

        LoadoutData loadoutData = LocalDataStorage.Instance.PlayerData.LoadoutData;
        UpgradesData upgradesData = LocalDataStorage.Instance.PlayerData.UpgradesData;

        int level = upgradesData.UpgradeData.FirstOrDefault(item => 
        item.FriendlyID == elementItem.FriendlyID).Level;
        _text.text = elementItem.Name + " " + level.ToString();

        List<ElementItem> equippedElements = loadoutData.EquippedElements;
        if (equippedElements.Contains(elementItem))
        {
            EquipElement(equippedElements);
        }
    }

    public void ToggleElement()
    {
        LoadoutData loadoutData = LocalDataStorage.Instance.PlayerData.LoadoutData;
        UpgradesData upgradesData = LocalDataStorage.Instance.PlayerData.UpgradesData;

        List<ElementItem> equippedElements = loadoutData.EquippedElements;
        if (!equippedElements.Contains(ElementItem))
        {
            if (EquipElement(equippedElements))
            {
                equippedElements.Add(ElementItem);
            }
        }
        else
        {
            if (equippedElements.Count >= upgradesData.UpgradeData.FirstOrDefault(item => item.ItemType == ItemType.Item).Level)
            {
                OnElementsChanged?.Invoke(true);
            }

            equippedElements.Remove(ElementItem);
            _image.color = new(_image.color.r, _image.color.g, _image.color.b, 100f / 255f);
        }

        if (equippedElements.Count == 0)
        {
            equippedElements = new();
        }

        LocalDataStorage.Instance.PlayerData.LoadoutData = new(equippedElements, 
            loadoutData.EquippedWeapon, loadoutData.WeaponInstance);
    }

    private bool EquipElement(List<ElementItem> equippedElements)
    {
        UpgradesData upgradesData = LocalDataStorage.Instance.PlayerData.UpgradesData;
        UpgradeData upgradeData = upgradesData.UpgradeData.FirstOrDefault(item => item.ItemType == ItemType.Item);
        bool equipped = false;
        if (upgradeData != null)
        {
            equipped = true;
            if (equippedElements.Count >= upgradeData.Level)
            {
                OnElementsChanged?.Invoke(false);
            }

            _image.color = new(_image.color.r, _image.color.g, _image.color.b, 1);
        }

        return equipped;
    }
}

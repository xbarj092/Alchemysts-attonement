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

        _image.sprite = elementItem.Icon;

        int level = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(item => item.FriendlyID == elementItem.FriendlyID).Level;
        _text.text = elementItem.Name + " " + level.ToString();

        List<ElementItem> equippedElements = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements;
        if (equippedElements.Contains(elementItem))
        {
            EquipElement(equippedElements);
        }
    }

    public void ToggleElement()
    {
        List<ElementItem> equippedElements = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements;
        if (!equippedElements.Contains(ElementItem))
        {
            equippedElements.Add(ElementItem);
            EquipElement(equippedElements);
        }
        else
        {
            if (equippedElements.Count >= LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(item => item.ItemType == ItemType.Item).Level)
            {
                OnElementsChanged?.Invoke(true);
            }

            equippedElements.Remove(ElementItem);
            _image.color = new(_image.color.r, _image.color.g, _image.color.b, 100f / 255f);
        }

        LocalDataStorage.Instance.PlayerData.LoadoutData = new(equippedElements, LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedWeapon);
    }

    private void EquipElement(List<ElementItem> equippedElements)
    {
        if (equippedElements.Count >= LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(item => item.ItemType == ItemType.Item).Level)
        {
            OnElementsChanged?.Invoke(false);
        }

        _image.color = new(_image.color.r, _image.color.g, _image.color.b, 1);
    }
}

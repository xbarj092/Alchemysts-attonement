using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadoutElements : MonoBehaviour
{
    [SerializeField] private List<LoadoutElement> _elements = new();

    private void Start()
    {
        InitElements();
    }

    private void OnDisable()
    {
        foreach (LoadoutElement element in _elements)
        {
            element.OnElementsChanged -= ChangeElements;
        }
    }

    private void InitElements()
    {
        List<string> friendlyIDs = new();
        LoadoutData loadoutData = LocalDataStorage.Instance.PlayerData.LoadoutData;
        UpgradesData upgradesData = LocalDataStorage.Instance.PlayerData.UpgradesData;

        foreach (UpgradeData upgradedItem in upgradesData.UpgradeData.Where(item => item.ItemType == ItemType.Element))
        {
            friendlyIDs.Add(upgradedItem.FriendlyID);
        }

        List<ElementItem> elementItems = LocalDataStorage.Instance.Catalog.CatalogItemList.Where(item => item.ItemType == ItemType.Element).Cast<ElementItem>().ToList();
        for (int i = 0; i < elementItems.Count; i++)
        {
            if (friendlyIDs.Contains(elementItems[i].FriendlyID))
            {
                _elements[i].gameObject.SetActive(true);
                _elements[i].Init(elementItems[i]);
                _elements[i].OnElementsChanged += ChangeElements;
            }
        }

        UpgradeData upgradeData = upgradesData.UpgradeData.FirstOrDefault(item => item.ItemType == ItemType.Item);
        if (upgradeData != null)
        {
            ChangeElements(loadoutData.EquippedElements.Count < upgradeData.Level);
        }
    }

    private void ChangeElements(bool changed)
    {
        LoadoutData loadoutData = LocalDataStorage.Instance.PlayerData.LoadoutData;
        foreach (LoadoutElement element in _elements)
        {
            if (!loadoutData.EquippedElements.Any(equippedElement => equippedElement.FriendlyID == element.ElementItem?.FriendlyID))
            {
                element.Button.interactable = changed;
            }
        }
    }
}

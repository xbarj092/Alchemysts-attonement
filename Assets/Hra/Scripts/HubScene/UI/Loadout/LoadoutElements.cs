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
        if (LocalDataStorage.Instance.PlayerData.LoadoutData == null)
        {
            FirstInit();
        }
        else
        {
            LoadInit();
        }
    }

    private void FirstInit()
    {
        List<string> friendlyIDs = new();
        foreach (UpgradeData upgradedItem in LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.Where(item => item.ItemType == ItemType.Element))
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
    }

    private void LoadInit()
    {

    }

    private void ChangeElements(bool changed)
    {
        foreach (LoadoutElement element in _elements)
        {
            if (!LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements.Any(equippedElement => equippedElement.FriendlyID == element.ElementItem?.FriendlyID))
            {
                element.Button.interactable = changed;
            }
        }
    }
}

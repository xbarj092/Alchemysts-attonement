using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadoutElements : MonoBehaviour
{
    [SerializeField] private List<LoadoutElement> _elements = new();

    private void Awake()
    {
        InitElements();
    }

    private void InitElements()
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
            }
        }
    }
}

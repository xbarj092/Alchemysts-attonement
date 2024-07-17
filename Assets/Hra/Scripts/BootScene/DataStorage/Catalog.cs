using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Catalog
{
    public List<ItemBase> CatalogItemList;

    private List<WeaponItem> _existingWeaponItems;
    private List<ElementItem> _existingElementItems;
    private List<ItemBase> _existingItems;

    private const string RESOURCE_PATH_WEAPON = "Items/Weapons";
    private const string RESOURCE_PATH_ELEMENTS = "Items/Elements";
    private const string RESOURCE_PATH_ITEMS = "Items/Items";

    public void InitializeCatalog()
    {
        _existingWeaponItems = Resources.LoadAll<WeaponItem>(RESOURCE_PATH_WEAPON).ToList();
        _existingElementItems = Resources.LoadAll<ElementItem>(RESOURCE_PATH_ELEMENTS).ToList();
        _existingItems = Resources.LoadAll<ItemBase>(RESOURCE_PATH_ITEMS).ToList();

        CatalogItemList.AddRange(_existingWeaponItems);
        CatalogItemList.AddRange(_existingElementItems);
        CatalogItemList.AddRange(_existingItems);
    }
}

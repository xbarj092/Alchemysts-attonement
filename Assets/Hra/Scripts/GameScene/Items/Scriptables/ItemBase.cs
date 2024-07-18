using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None = 0,
    Item = 1,
    Weapon = 2,
    Element = 3
}

[CreateAssetMenu(fileName = "New base item", menuName = "Item/BaseItem")]
public class ItemBase : ScriptableObject
{
    public ItemType ItemType;

    public string Name;
    public string FriendlyID;
    public string Description;

    public Sprite Icon;

    public List<int> UpgradePrices = new();
}

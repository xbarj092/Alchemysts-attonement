using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New base item", menuName = "Item/BaseItem")]
public class ItemBase : ScriptableObject
{
    public string Name;
    public string FriendlyID;
    public string Description;

    public Sprite Icon;

    public List<int> UpgradePrices = new();
}

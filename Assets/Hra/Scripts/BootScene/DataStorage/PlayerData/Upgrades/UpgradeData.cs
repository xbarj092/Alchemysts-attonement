using System;

[Serializable]
public class UpgradeData
{
    public ItemType ItemType;
    public string FriendlyID = null;
    public bool Purchased = false;
    public int Level = 0;

    public UpgradeData(ItemType itemType, string friendlyID, bool purchased, int level)
    {
        ItemType = itemType;
        FriendlyID = friendlyID;
        Purchased = purchased;
        Level = level;
    }
}

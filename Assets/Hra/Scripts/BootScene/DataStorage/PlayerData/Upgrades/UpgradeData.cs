using System;

[Serializable]
public class UpgradeData
{
    public string FriendlyID = null;
    public bool Purchased = false;
    public int Level = 0;

    public UpgradeData(string friendlyID, bool purchased, int level)
    {
        FriendlyID = friendlyID;
        Purchased = purchased;
        Level = level;
    }
}

using System;

[Serializable]
public class UpgradeData
{
    public string FriendlyID;
    public bool Purchased;
    public int Level;

    public UpgradeData(string friendlyID, bool purchased, int level)
    {
        FriendlyID = friendlyID;
        Purchased = purchased;
        Level = level;
    }
}

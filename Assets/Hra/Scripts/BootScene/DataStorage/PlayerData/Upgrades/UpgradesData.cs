using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradesData
{
    public List<UpgradeData> UpgradeData;

    public UpgradesData(List<UpgradeData> upgradeData)
    {
        UpgradeData = upgradeData;
    }
}

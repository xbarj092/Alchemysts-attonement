using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private UpgradesData _upgradesData;
    public UpgradesData UpgradesData
    {
        get => _upgradesData;
        set
        {
            _upgradesData = value;
            DataEvents.OnUpgradesDataChangedInvoke(_upgradesData);
        }
    }

    [SerializeField] private LoadoutData _loadoutData;
    public LoadoutData LoadoutData
    {
        get => _loadoutData;
        set
        {
            _loadoutData = value;
            DataEvents.OnLoadoutDataChangedInvoke(_loadoutData);
        }
    }
}

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

    [SerializeField] private CurrencyData _currencyData;
    public CurrencyData CurrencyData
    {
        get => _currencyData;
        set
        {
            _currencyData = value;
            DataEvents.OnCurrencyDataChangedInvoke(_currencyData);
        }
    }

    [SerializeField] private PlayerStats _playerStats;
    public PlayerStats PlayerStats
    {
        get => _playerStats;
        set
        {
            _playerStats = value;
            DataEvents.OnPlayerStatsChangedInvoke(_playerStats);
        }
    }
}

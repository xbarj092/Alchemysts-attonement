using System;
using UnityEditor;

public static class DataEvents
{
    public static event Action<UpgradesData> OnUpgradesDataChanged;
    public static void OnUpgradesDataChangedInvoke(UpgradesData upgradesData)
    {
        OnUpgradesDataChanged?.Invoke(upgradesData);
    }

    public static event Action<LoadoutData> OnLoadoutDataChanged;
    public static void OnLoadoutDataChangedInvoke(LoadoutData loadoutData)
    {
        OnLoadoutDataChanged?.Invoke(loadoutData);
    }

    public static event Action<CurrencyData> OnCurrencyDataChanged;
    public static void OnCurrencyDataChangedInvoke(CurrencyData currencyData)
    {
        OnCurrencyDataChanged?.Invoke(currencyData);
    }

    public static event Action<PlayerStats> OnPlayerStatsChanged;
    public static void OnPlayerStatsChangedInvoke(PlayerStats playerStats)
    {
        OnPlayerStatsChanged?.Invoke(playerStats);
    }
}

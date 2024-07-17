using System;

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
}

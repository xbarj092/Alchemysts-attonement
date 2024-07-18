using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutScreen : GameScreen
{
    [SerializeField] private LoadoutElements _loadoutElements;
    [SerializeField] private LoadoutWeapon _loadoutWeapon;
    [SerializeField] private LoadoutStats _loadoutStats;

    private void OnEnable()
    {
        DataEvents.OnLoadoutDataChanged += UpdateLoadoutStats;
    }

    private void OnDisable()
    {
        DataEvents.OnLoadoutDataChanged -= UpdateLoadoutStats;
    }

    private void UpdateLoadoutStats(LoadoutData loadoutData)
    {
        _loadoutStats.UpdateStats();
    }
}

using UnityEngine;

public class LoadoutScreen : GameScreen
{
    [SerializeField] private LoadoutElements _loadoutElements;
    [SerializeField] private LoadoutWeapon _loadoutWeapon;
    [SerializeField] private LoadoutStats _loadoutStats;

    private void OnEnable()
    {
        DataEvents.OnLoadoutDataChanged += UpdateLoadoutData;
    }

    private void OnDisable()
    {
        DataEvents.OnLoadoutDataChanged -= UpdateLoadoutData;
    }

    private void UpdateLoadoutData(LoadoutData loadoutData)
    {
        _loadoutWeapon.ChangeWeaponText();
        _loadoutStats.UpdateStats();
    }
}

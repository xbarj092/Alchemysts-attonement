using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public List<BaseWeapon> ValidWeapons = new();

    private void OnEnable()
    {
        DataEvents.OnLoadoutDataChanged += SetWeapon;
    }

    private void OnDisable()
    {
        DataEvents.OnLoadoutDataChanged -= SetWeapon;
    }

    private void SetWeapon(LoadoutData loadoutData)
    {
        foreach (BaseWeapon weapon in GetComponentsInChildren<BaseWeapon>())
        {
            Destroy(weapon.gameObject);
        }

        Instantiate(GetWeapon(loadoutData), transform.position, transform.root.rotation, transform);
    }

    private BaseWeapon GetWeapon(LoadoutData loadoutData)
    {
        foreach (BaseWeapon weapon in ValidWeapons)
        {
            if (weapon.WeaponItem.FriendlyID == loadoutData.EquippedWeapon.FriendlyID)
            {
                return weapon;
            }
        }

        return null;
    }
}

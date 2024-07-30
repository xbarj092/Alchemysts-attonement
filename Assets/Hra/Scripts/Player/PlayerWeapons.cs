using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public List<BaseWeapon> ValidWeapons = new();

    public event Action<BaseWeapon> OnWeaponChanged;

    private void OnEnable()
    {
        DataEvents.OnLoadoutDataChanged += SetWeapon;
    }

    private void OnDisable()
    {
        DataEvents.OnLoadoutDataChanged -= SetWeapon;
    }

    public void SetWeapon(LoadoutData loadoutData)
    {
        foreach (BaseWeapon weapon in GetComponentsInChildren<BaseWeapon>())
        {
            Destroy(weapon.gameObject);
        }

        BaseWeapon newWeapon = GetWeapon(loadoutData);
        BaseWeapon instantiatedWeapon = Instantiate(newWeapon, transform.position, transform.root.rotation, transform);
        StartCoroutine(DelayedWeaponChange(instantiatedWeapon));
    }

    private IEnumerator DelayedWeaponChange(BaseWeapon newWeapon)
    {
        yield return null;
        OnWeaponChanged?.Invoke(newWeapon);
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

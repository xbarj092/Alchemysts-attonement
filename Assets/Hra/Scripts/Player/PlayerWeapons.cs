using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public List<BaseWeapon> ValidWeapons = new();

    public event Action OnWeaponChanged;

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
        StartCoroutine(DelayedWeaponChange());
    }

    private IEnumerator DelayedWeaponChange()
    {
        yield return null;
        OnWeaponChanged?.Invoke();
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

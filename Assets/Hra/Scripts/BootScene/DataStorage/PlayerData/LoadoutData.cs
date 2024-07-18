using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoadoutData
{
    public List<ElementItem> EquippedElements;
    public WeaponItem EquippedWeapon;
    public WeaponInstance WeaponInstance;

    public LoadoutData(List<ElementItem> equippedElements, WeaponItem equippedWeapon, WeaponInstance weaponInstance)
    {
        EquippedElements = equippedElements;
        EquippedWeapon = equippedWeapon;
        WeaponInstance = weaponInstance;
    }
}

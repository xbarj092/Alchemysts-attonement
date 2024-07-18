using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoadoutData
{
    public List<ElementItem> EquippedElements;
    public WeaponItem EquippedWeapon;

    public LoadoutData(List<ElementItem> equippedElements, WeaponItem equippedWeapon)
    {
        EquippedElements = equippedElements;
        EquippedWeapon = equippedWeapon;
    }
}

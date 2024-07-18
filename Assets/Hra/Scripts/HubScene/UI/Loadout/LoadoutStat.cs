using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponStat
{
    None = 0,
    Damage = 1,
    Range = 2,
    AttackRate = 3,
    EnemySlow = 4,
    Dot = 5,
    Heal = 6,
    ChainDamage = 7
}

public class LoadoutStat : MonoBehaviour
{
    [SerializeField] private Image _statImage;
    [SerializeField] private TMP_Text _statText;
    [SerializeField] private bool _multiplier;

    public WeaponStat WeaponStat;

    public void Init(float specialEffectValue)
    {
        WeaponItem equippedWeapon = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedWeapon;
        float weaponValue = equippedWeapon.GetValueFromStat(WeaponStat);
        float updatedValue = weaponValue;
        if (weaponValue > 0)
        {
            updatedValue = _multiplier ? specialEffectValue * weaponValue : specialEffectValue + weaponValue;
        }
        else
        {
            updatedValue = specialEffectValue;
        }

        _statText.text = updatedValue.ToString();
    }
}

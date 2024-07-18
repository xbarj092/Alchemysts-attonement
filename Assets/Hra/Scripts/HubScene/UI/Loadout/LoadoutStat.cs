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

    private float _statValue;

    public WeaponStat WeaponStat;

    public void Init(float specialEffectValue, bool special = false, bool init = false)
    {
        WeaponItem equippedWeapon = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedWeapon;
        float weaponValue = equippedWeapon.GetValueFromStat(WeaponStat);
        float updatedValue = weaponValue;
        if (init)
        {
            if (special)
            {
                if (weaponValue > 0)
                {
                    updatedValue = _multiplier ? specialEffectValue * weaponValue : specialEffectValue + weaponValue;
                }
                else
                {
                    updatedValue = specialEffectValue;
                }
            }
            else
            {
                updatedValue = weaponValue;
            }
        }
        else
        {
            if (special)
            {
                if (weaponValue > 0)
                {
                    updatedValue = _multiplier ? specialEffectValue * _statValue : specialEffectValue + _statValue;
                }
                else
                {
                    updatedValue = specialEffectValue;
                }
            }
            else
            {
                updatedValue = _statValue;
            }
        }
        
        _statValue = updatedValue;
        _statText.text = updatedValue.ToString();
    }
}

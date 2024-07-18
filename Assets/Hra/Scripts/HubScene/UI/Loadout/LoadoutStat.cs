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

    public float StatValue;
    public WeaponStat WeaponStat;

    public void Init(float specialEffectValue, bool special = false, bool init = false)
    {
        WeaponItem equippedWeapon = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedWeapon;
        if (equippedWeapon == null)
        {
            return;
        }

        float weaponValue = equippedWeapon.GetValueFromStat(WeaponStat);
        StatValue = CalculateUpdatedValue(weaponValue, specialEffectValue, special, init);
        _statText.text = StatValue.ToString();
    }

    private float CalculateUpdatedValue(float weaponValue, float specialEffectValue, bool special, bool init)
    {
        float baseValue = init ? weaponValue : StatValue;

        if (!special)
        {
            return baseValue;
        }

        if (weaponValue > 0)
        {
            return _multiplier ? specialEffectValue * baseValue : specialEffectValue + baseValue;
        }

        return specialEffectValue;
    }
}

using System;

[Serializable]
public class WeaponInstance
{
    public float Damage;
    public float Range;
    public float AttackRate;
    public float Dot;
    public float ChainDamage;
    public float EnemySlow;
    public float Heal;

    public WeaponInstance()
    {
    }

    public WeaponInstance(float damage, float range, float attackRate, float dot, float chainDamage, float enemySlow, float heal)
    {
        Damage = damage;
        Range = range;
        AttackRate = attackRate;
        Dot = dot;
        ChainDamage = chainDamage;
        EnemySlow = enemySlow;
        Heal = heal;
    }
}

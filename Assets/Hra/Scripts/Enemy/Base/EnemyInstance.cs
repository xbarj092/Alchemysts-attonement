using System;

[Serializable]
public class EnemyInstance
{
    public float CurrentHealth;
    public float MaxHealth;
    public float MovementSpeed;

    public float TouchDamage;
    public float DropsCoins;
    public float DropsShadows;

    public WeaponItem Weapon;

    public EnemyInstance(EnemyBase enemyStats)
    {
        CurrentHealth = enemyStats.Health;
        MaxHealth = enemyStats.Health;
        MovementSpeed = enemyStats.MovementSpeed;
        
        TouchDamage = enemyStats.TouchDamage;
        DropsCoins = enemyStats.DropsCoins;
        DropsShadows = enemyStats.DropsShadows;
    
        Weapon = enemyStats.Weapon;
    }
}

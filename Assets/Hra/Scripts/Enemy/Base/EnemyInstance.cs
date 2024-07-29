using System;

[Serializable]
public class EnemyInstance
{
    public float CurrentHealth;
    public float MaxHealth;
    public float MovementSpeed;

    public float TouchDamage;
    public int DropsCoins;
    public int DropsShadows;

    public WeaponItem Weapon;

    public EnemyInstance(EnemyBase enemyStats)
    {
        CurrentHealth = enemyStats.Health;
        MaxHealth = enemyStats.Health;
        MovementSpeed = enemyStats.MovementSpeed;
        
        DropsCoins = enemyStats.DropsCoins;
        DropsShadows = enemyStats.DropsShadows;
    
        Weapon = enemyStats.Weapon;
    }
}

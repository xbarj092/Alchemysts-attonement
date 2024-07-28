using System;

[Serializable]
public class EnemyInstance
{
    public float CurrentHealth;
    public float MaxHealth;
    public float Damage;
    public float AttackRate;
    public float MovementSpeed;

    public int DropsCoins;
    public int DropsShadows;

    public EnemyInstance(EnemyBase enemyStats)
    {
        CurrentHealth = enemyStats.Health;
        MaxHealth = enemyStats.Health;
        Damage = enemyStats.Damage;
        AttackRate = enemyStats.AttackRate;
        MovementSpeed = enemyStats.MovementSpeed;

        DropsCoins = enemyStats.DropsCoins;
        DropsShadows = enemyStats.DropsShadows;
    }
}

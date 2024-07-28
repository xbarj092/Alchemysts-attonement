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

    public bool IsFreezeApplied;
    public bool IsDoTApplied;
    public bool IsChainApplied;

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

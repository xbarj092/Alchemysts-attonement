public class EnemyInstance
{
    public float CurrentHealth;
    public float MaxHealth;
    public float Damage;
    public int DropsCoins;
    public int DropsShadows;

    public EnemyInstance(EnemyBase enemyStats)
    {
        CurrentHealth = enemyStats.Health;
        MaxHealth = enemyStats.Health;
        Damage = enemyStats.Damage;
        DropsCoins = enemyStats.DropsCoins;
        DropsShadows = enemyStats.DropsShadows;
    }
}

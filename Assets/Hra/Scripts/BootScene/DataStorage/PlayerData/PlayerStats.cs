using System;

[Serializable]
public class PlayerStats
{
    public float CurrentHealth;
    public float MaxHealth;

    public PlayerStats(float currentHealth, float maxHealth)
    {
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
    }
}

using System;

[Serializable]
public class CurrencyData
{
    public int Coins;
    public float CurrentShadows;
    public float MaxShadows;

    public CurrencyData(int coins, float currentShadows, float maxShadows)
    {
        Coins = coins;
        CurrentShadows = currentShadows;
        MaxShadows = maxShadows;
    }
}

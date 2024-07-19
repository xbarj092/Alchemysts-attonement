using System;

[Serializable]
public class CurrencyData
{
    public int Coins;
    public float CurrentShadows;
    public float NextShadows;

    public CurrencyData(int coins, float currentShadows, float nextShadows)
    {
        Coins = coins;
        CurrentShadows = currentShadows;
        NextShadows = nextShadows;
    }
}

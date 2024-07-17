using System;

[Serializable]
public class CurrencyData
{
    public int Coins;
    public int CurrentShadows;
    public int NextShadows;

    public CurrencyData(int coins, int currentShadows, int nextShadows)
    {
        Coins = coins;
        CurrentShadows = currentShadows;
        NextShadows = nextShadows;
    }
}

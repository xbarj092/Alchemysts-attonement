using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ShadowsBar _shadowsBar;
    [SerializeField] private Currency _currency;

    private void Start()
    {
        UpdateCurrencyData(LocalDataStorage.Instance.PlayerData.CurrencyData);
        UpdatePlayerStats(LocalDataStorage.Instance.PlayerData.PlayerStats);
    }

    private void OnEnable()
    {
        DataEvents.OnCurrencyDataChanged += UpdateCurrencyData;
        DataEvents.OnPlayerStatsChanged += UpdatePlayerStats;
    }

    private void OnDisable()
    {
        DataEvents.OnCurrencyDataChanged -= UpdateCurrencyData;
        DataEvents.OnPlayerStatsChanged -= UpdatePlayerStats;
    }

    private void UpdateCurrencyData(CurrencyData data)
    {
        _currency.SetCurrencyText(data.Coins);
        _shadowsBar.SetShadowsBar(data.CurrentShadows, data.NextShadows);
    }

    private void UpdatePlayerStats(PlayerStats stats)
    {
        _healthBar.SetHealth(stats.CurrentHealth, stats.MaxHealth);
    }
}

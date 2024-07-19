using UnityEngine;

public class UpgradesScreen : GameScreen
{
    [SerializeField] private Currency _currency;

    private void Start()
    {
        UpdateCurrencyData(LocalDataStorage.Instance.PlayerData.CurrencyData);
    }

    private void OnEnable()
    {
        DataEvents.OnCurrencyDataChanged += UpdateCurrencyData;
    }

    private void OnDisable()
    {
        DataEvents.OnCurrencyDataChanged -= UpdateCurrencyData;
    }

    private void UpdateCurrencyData(CurrencyData data)
    {
        _currency.SetCurrencyText(data.Coins);
    }
}

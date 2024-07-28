using UnityEngine;

public class Coin : MonoBehaviour
{
    private int _amount;

    public void Init(int cointAmount)
    {
        _amount = cointAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
            currencyData.Coins += _amount;
            LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
            Destroy(gameObject);
        }
    }
}

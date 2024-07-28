using UnityEngine;

public class Shadow : MonoBehaviour
{
    private int _amount;

    public void Init(int shadowAmount)
    {
        _amount = shadowAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
            currencyData.CurrentShadows += _amount;
            LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
            Destroy(gameObject);
        }
    }
}

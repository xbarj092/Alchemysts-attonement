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
            float shadowsAfterPickup = currencyData.CurrentShadows + _amount;
            if (shadowsAfterPickup > currencyData.MaxShadows)
            {
                shadowsAfterPickup = currencyData.MaxShadows;
            }
            
            currencyData.CurrentShadows = shadowsAfterPickup;
            LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
            Destroy(gameObject);
        }
    }
}

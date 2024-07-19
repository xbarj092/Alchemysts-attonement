using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyText;

    public void SetCurrencyText(int coins)
    {
        _currencyText.text = coins.ToString();
    }
}

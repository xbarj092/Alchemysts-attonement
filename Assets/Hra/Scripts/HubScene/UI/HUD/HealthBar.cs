using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFill;

    public void SetPlayerHealth(float currentHealth, float maxHealth)
    {
        _healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}

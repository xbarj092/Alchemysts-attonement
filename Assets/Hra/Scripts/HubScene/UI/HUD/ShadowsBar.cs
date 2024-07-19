using UnityEngine;
using UnityEngine.UI;

public class ShadowsBar : MonoBehaviour
{
    [SerializeField] private Image _shadowsBarFill;

    public void SetShadowsBar(float currentShadows, float nextShadows)
    {
        _shadowsBarFill.fillAmount = currentShadows / nextShadows;
    }
}

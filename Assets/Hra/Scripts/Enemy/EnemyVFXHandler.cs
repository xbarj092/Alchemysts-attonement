using UnityEngine;

public class EnemyVFXHandler : MonoBehaviour
{
    [SerializeField] private Transform _slowEffect;
    [SerializeField] private Transform _dotEffect;
    [SerializeField] private Transform _chainEffect;

    public void SetEffect(SpecialEffect effect, bool enabled)
    {
        switch (effect)
        {
            case SpecialEffect.EnemySlow:
                _slowEffect?.gameObject.SetActive(enabled);
                break;
            case SpecialEffect.Dot:
                _dotEffect?.gameObject.SetActive(enabled);
                break;
            case SpecialEffect.ChainDamage:
                _chainEffect?.gameObject.SetActive(enabled);
                break;
        }
    }
}

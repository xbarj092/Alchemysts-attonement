using UnityEngine;

public enum WeaponVFX
{
    None = 0,
    Hit = 1,
    Swing = 2,
    Shoot = 3
}

public class WeaponVFXHandler : MonoBehaviour
{
    [SerializeField] private Transform _hitEffect;
    [SerializeField] private Transform _swingEffect;
    [SerializeField] private Transform _shootEffect;

    public void SetEffect(WeaponVFX weaponVFX, bool enabled)
    {
        switch (weaponVFX)
        {
            case WeaponVFX.Hit:
                _hitEffect?.gameObject.SetActive(enabled);
                break;
            case WeaponVFX.Swing:
                _swingEffect?.gameObject.SetActive(enabled);
                break;
            case WeaponVFX.Shoot:
                _shootEffect?.gameObject.SetActive(enabled);
                break;
        }

    }
}

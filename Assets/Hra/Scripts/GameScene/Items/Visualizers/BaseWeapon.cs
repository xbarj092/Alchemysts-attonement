using System.Collections;
using UnityEngine;
using System;

public enum WeaponStates
{
    None,
    Using,
    CoolDown,
    Ready
}

public class BaseWeapon : MonoBehaviour
{
    [field: SerializeField] public WeaponItem WeaponItem { get; protected set; }
    [field: SerializeField] public WeaponStates State { get; protected set; } = WeaponStates.Ready;
    [field: SerializeField] public GameObject Visual { get; protected set; }
    [field: SerializeField] public Entity Holder { get; protected set; }
    public bool CanUse { get; protected set; } = true;

    [SerializeField] protected WeaponVFXHandler _weaponVFXHandler;

    protected ElementHandler _elementHandler = new();

    public event Action<BaseWeapon, bool> OnWeaponUsed;
    public event Action<WeaponStates> OnStateChanged;
    public event Action<float> OnCoolDownSwipe;

    protected void TriggerCoolDownSwipe(float duration)
    {
        OnCoolDownSwipe?.Invoke(duration);
    }

    protected void TriggerOnWeaponUsed(BaseWeapon self)
    {
        OnWeaponUsed?.Invoke(self, true);
    }

    protected void ChangeGunState(WeaponStates newState)
    {
        State = newState;
        OnStateChanged?.Invoke(newState);
    }

    protected virtual IEnumerator CoolDown()
    {
        yield return null;
        ChangeGunState(WeaponStates.Ready);
    }

    public void SetHolder(Entity holder)
    {
        Holder = holder;
    }
}

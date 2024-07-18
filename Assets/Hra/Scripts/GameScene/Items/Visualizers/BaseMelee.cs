using System.Collections;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
    public void Use()
    {
        if (!CanUse) return;

        CanUse = false;
        StartCoroutine(Activate());
    }

    public IEnumerator Activate()
    {
        TriggerCoolDownSwipe(1 / WeaponInstance.AttackRate);
        yield return StartCoroutine(Swing());
        yield return StartCoroutine(CoolDown());
        CanUse = true;
    }

    protected IEnumerator Swing()
    {
        ChangeGunState(WeaponStates.Using);
        yield return null;
    }

    public void OnPlaySwingVisual()
    {
    }

    private void DamageTarget(Transform targetGameObject, RaycastHit hitInfo)
    {
    }

    protected void PlayOnHitVisual(Vector3 position, Quaternion rotation)
    {
    }
}

using System.Collections;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
    [SerializeField] private Collision _colliderObject;

    private void OnEnable()
    {
        _colliderObject.OnTriggerEnter += OnCollisionTriggered;
    }

    private void OnDisable()
    {
        _colliderObject.OnTriggerEnter -= OnCollisionTriggered;
    }

    private void OnCollisionTriggered(Collider2D collision)
    {
        if (State == WeaponStates.Using && collision.gameObject.CompareTag(GlobalConstants.Tags.Enemy.ToString()))
        {
            DamageTarget(collision.gameObject.transform);
        }
    }

    public void Use()
    {
        if (!CanUse) return;

        StartCoroutine(Activate());
    }

    public IEnumerator Activate()
    {
        CanUse = false;
        TriggerCoolDownSwipe(1 / LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate);
        yield return StartCoroutine(Swing());
        yield return StartCoroutine(CoolDown());
        CanUse = true;
    }

    protected IEnumerator Swing()
    {
        ChangeGunState(WeaponStates.Using);
        yield return new WaitForSeconds(1 / LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.Damage);
    }

    public void OnPlaySwingVisual()
    {
    }

    private void DamageTarget(Transform targetGameObject)
    {
        targetGameObject.GetComponent<Enemy>().Damage(LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.Damage);
    }

    protected void PlayOnHitVisual(Vector3 position, Quaternion rotation)
    {
    }
}

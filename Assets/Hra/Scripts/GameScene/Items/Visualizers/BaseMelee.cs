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
        PlayOnSwingVisual();
        yield return new WaitForSeconds(1 / LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate);
    }

    public void PlayOnSwingVisual()
    {
        _weaponVFXHandler.SetEffect(WeaponVFX.Swing, true);
    }

    private void DamageTarget(Transform targetGameObject)
    {
        if (Holder is PlayerController)
        {
            Enemy enemy = targetGameObject.GetComponent<Enemy>();
            enemy.Damage(LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.Damage);
            _elementHandler.ApplyElements(enemy);
        }
        else if (Holder is Enemy enemy)
        {
            PlayerController player = targetGameObject.GetComponent<PlayerController>();
            player.Damage(enemy.EnemyInstance.Weapon.Damage);
        }

        PlayOnHitVisual();
    }

    protected void PlayOnHitVisual()
    {
        _weaponVFXHandler.SetEffect(WeaponVFX.Hit, true);
    }
}

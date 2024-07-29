using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
    [SerializeField] private Collision _colliderObject;
    private List<Collider2D> _collidersInRange = new();
    private List<Collider2D> _damagedEnemies = new();

    private void OnEnable()
    {
        _colliderObject.OnTriggerEnter += OnCollisionTriggered;
        _colliderObject.OnTriggerExit += OnCollisionExitted;
    }

    private void OnDisable()
    {
        _colliderObject.OnTriggerEnter -= OnCollisionTriggered;
        _colliderObject.OnTriggerExit -= OnCollisionExitted;
    }

    private void OnCollisionTriggered(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.Tags.Enemy.ToString()))
        {
            if (State == WeaponStates.Using && !_damagedEnemies.Contains(collision))
            {
                _damagedEnemies.Add(collision);
                DamageTarget(collision.transform);
            }

            _collidersInRange.Add(collision);
        }
    }

    private void OnCollisionExitted(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.Tags.Enemy.ToString()))
        {
            _collidersInRange.Remove(collision);
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
        _damagedEnemies.Clear();
    }

    protected IEnumerator Swing()
    {
        ChangeGunState(WeaponStates.Using);
        PlayOnSwingVisual();
        ApplyDamageToCollidersInRange();
        yield return new WaitForSeconds(1 / LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate);
    }

    public void PlayOnSwingVisual()
    {
        _weaponVFXHandler.SetEffect(WeaponVFX.Swing, true);
    }

    private void ApplyDamageToCollidersInRange()
    {
        foreach (Collider2D collider in new List<Collider2D>(_collidersInRange))
        {
            if (collider != null && collider.gameObject.activeInHierarchy && !_damagedEnemies.Contains(collider))
            {
                _damagedEnemies.Add(collider);
                DamageTarget(collider.transform);
            }
            else
            {
                if (_damagedEnemies.Contains(collider))
                {
                    _damagedEnemies.Remove(collider);
                }

                _collidersInRange.Remove(collider);
            }
        }
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

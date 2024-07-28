using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : BaseWeapon
{
    [SerializeField] private Projectile _projectilePrefab;

    private List<Projectile> _projectiles = new();

    public void Use()
    {
        if (!CanUse) return;

        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        CanUse = false;
        TriggerCoolDownSwipe(1 / LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate);
        yield return StartCoroutine(Shoot());
        yield return StartCoroutine(CoolDown());
        CanUse = true;
    }

    private IEnumerator Shoot()
    {
        Projectile newProjectile = Instantiate(_projectilePrefab, Holder.transform);
        float damage = 0;
        float waitTime = 0;
        Vector2 shootDirection = Vector2.right;

        if (Holder is Enemy enemy)
        {
            damage = enemy.EnemyInstance.Weapon.Damage;
            waitTime = enemy.EnemyInstance.Weapon.AttacksPerSecond;
            Vector2 targetPosition = FindObjectOfType<PlayerController>().transform.position;
            shootDirection = (targetPosition - (Vector2)Holder.transform.position).normalized;
        }
        else if (Holder is PlayerController)
        {
            damage = LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.Damage;
            waitTime = LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate;
        }

        newProjectile.Init(Holder, damage);
        newProjectile.Launch(shootDirection);
        _projectiles.Add(newProjectile);
        ChangeGunState(WeaponStates.Using);
        PlayOnShootVisual();
        yield return new WaitForSeconds(1 / waitTime);
    }

    public void PlayOnShootVisual()
    {
        _weaponVFXHandler.SetEffect(WeaponVFX.Shoot, true);
    }
}

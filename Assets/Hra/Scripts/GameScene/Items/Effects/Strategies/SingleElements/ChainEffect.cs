using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChainEffect : IEnemyEffect
{
    private ElementHandler _elementHandler = new();

    private const int TARGET_AMOUNT = 5;
    private const float EFFECT_RANGE = 3f;

    public IEnumerator ApplyEffect(Enemy initialTarget, ElementItem element)
    {
        List<Enemy> affectedEnemies = new();
        Transform currentTargetTransform = initialTarget != null && !initialTarget.IsDead ? initialTarget.transform : null;
        UpgradeData upgradeData = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == element.FriendlyID);
        if (upgradeData == null)
        {
            yield break;
        }

        float damage = element.SpecialEffects[SpecialEffect.ChainDamage][upgradeData.Level - 1];

        for (int i = 0; i < TARGET_AMOUNT; i++)
        {
            if (currentTargetTransform == null)
            {
                Enemy newInitialTarget = FindClosestEnemy(GameObject.FindObjectOfType<MeleeWeapon>().transform.position, EFFECT_RANGE);
                if (newInitialTarget == null)
                {
                    ResetChainEffect(affectedEnemies);
                    yield break;
                }
                currentTargetTransform = newInitialTarget.transform;
            }

            try
            {
                Enemy nextTarget = FindClosestEnemy(currentTargetTransform.position, EFFECT_RANGE);
                if (nextTarget == null)
                {
                    ResetChainEffect(affectedEnemies);
                    yield break;
                }

                nextTarget.IsChainApplied = true;
                affectedEnemies.Add(nextTarget);

                nextTarget.Damage(damage);
                if (!nextTarget.IsDead)
                {
                    _elementHandler.ApplyElements(nextTarget, true);
                }
                currentTargetTransform = nextTarget.transform;
            }
            catch (MissingReferenceException)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.2f);
        }

        ResetChainEffect(affectedEnemies);
    }

    private Enemy FindClosestEnemy(Vector3 position, float range)
    {
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Enemy enemy in EnemyManager.Instance.GetAllEnemies())
        {
            if (enemy.IsChainApplied || enemy.IsDead)
            {
                continue;
            }

            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < closestDistance && distance <= range)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void ResetChainEffect(List<Enemy> affectedEnemies)
    {
        foreach (Enemy enemy in affectedEnemies)
        {
            if (enemy != null)
            {
                enemy.IsChainApplied = false;
            }
        }
    }
}

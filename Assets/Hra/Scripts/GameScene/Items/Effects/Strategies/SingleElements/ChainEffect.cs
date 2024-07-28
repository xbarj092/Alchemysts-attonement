using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChainEffect : IEnemyEffect
{
    private ElementHandler _elementHandler = new();

    private const int TARGET_AMOUNT = 5;

    public IEnumerator ApplyEffect(Enemy initialTarget, ElementItem element)
    {
        List<Enemy> affectedEnemies = new();
        Transform currentTargetTransform = initialTarget.transform;
        UpgradeData upgradeData = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == element.FriendlyID);
        if (upgradeData == null)
        {
            yield break;
        }

        float damage = element.SpecialEffects[SpecialEffect.ChainDamage][upgradeData.Level - 1];

        for (int i = 0; i < TARGET_AMOUNT; i++)
        {
            try
            {
                Enemy nextTarget = FindClosestEnemy(currentTargetTransform.position);
                if (nextTarget == null)
                {
                    ResetChainEffect(affectedEnemies);
                    yield break;
                }

                nextTarget.EnemyInstance.IsChainApplied = true;
                nextTarget.Damage(damage);
                if (initialTarget != nextTarget)
                {
                    _elementHandler.ApplyElements(nextTarget, true);
                }
                affectedEnemies.Add(nextTarget);
                currentTargetTransform = nextTarget.transform;
            }
            catch (MissingReferenceException)
            {
                ResetChainEffect(affectedEnemies);
                yield break;
            }

            yield return new WaitForSeconds(0.2f);
        }

        ResetChainEffect(affectedEnemies);
    }

    private Enemy FindClosestEnemy(Vector3 position)
    {
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Enemy enemy in EnemyManager.Instance.GetAllEnemies())
        {
            if (enemy.EnemyInstance.IsChainApplied)
            {
                continue;
            }

            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < closestDistance)
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
            enemy.EnemyInstance.IsChainApplied = false;
        }
    }
}

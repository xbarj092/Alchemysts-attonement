using System.Collections;
using System.Linq;
using UnityEngine;

public class FrostEffect : IEnemyEffect
{
    public IEnumerator ApplyEffect(Enemy target, ElementItem element)
    {
        if (target.EnemyInstance.IsFreezeApplied)
        {
            yield break;
        }

        UpgradeData upgradeData = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == element.FriendlyID);
        if (upgradeData == null)
        {
            yield break;
        }

        float effectMultiplier = 1 - element.SpecialEffects[SpecialEffect.EnemySlow][upgradeData.Level - 1];
        float originalMovementSpeed = target.EnemyInstance.MovementSpeed;
        float originalAttackRate = target.EnemyInstance.AttackRate;

        target.EnemyInstance.MovementSpeed *= effectMultiplier;
        target.EnemyInstance.AttackRate *= effectMultiplier;
        target.EnemyInstance.IsFreezeApplied = true;

        yield return new WaitForSeconds(2);

        target.EnemyInstance.MovementSpeed = originalMovementSpeed;
        target.EnemyInstance.AttackRate = originalAttackRate;
        target.EnemyInstance.IsFreezeApplied = false;
    }
}

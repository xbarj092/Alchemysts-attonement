using System.Collections;
using System.Linq;
using UnityEngine;

public class DoTEffect : IEnemyEffect
{
    private const int TICK_AMOUNT = 5;
    private const int DOT_DURATION = 5;

    public IEnumerator ApplyEffect(Enemy target, ElementItem element)
    {
        if (target.EnemyInstance.IsDoTApplied)
        {
            yield break;
        }

        UpgradeData upgradeData = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == element.FriendlyID);
        if (upgradeData == null)
        {
            yield break;
        }

        target.EnemyInstance.IsDoTApplied = true;
        float damagePerTick = element.SpecialEffects[SpecialEffect.Dot][upgradeData.Level - 1] / TICK_AMOUNT;

        for (int i = 0; i < TICK_AMOUNT; i++)
        {
            target.Damage(damagePerTick);
            yield return new WaitForSeconds(DOT_DURATION / TICK_AMOUNT);
        }

        target.EnemyInstance.IsDoTApplied = false;
    }
}

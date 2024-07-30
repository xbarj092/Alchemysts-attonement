using System.Collections;
using System.Linq;

public class LifeStealEffect : IPlayerEffect
{
    public IEnumerator ApplyEffect(PlayerStats target, ElementItem element)
    {
        yield return null;
        UpgradeData upgradeData = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(upgrade => upgrade.FriendlyID == element.FriendlyID);
        float hpAfterHeal = target.CurrentHealth + element.SpecialEffects[SpecialEffect.Heal][upgradeData.Level - 1];
        if (hpAfterHeal > target.MaxHealth)
        {
            hpAfterHeal = target.MaxHealth;
        }

        target.CurrentHealth = hpAfterHeal;
        LocalDataStorage.Instance.PlayerData.PlayerStats = target;
    }
}

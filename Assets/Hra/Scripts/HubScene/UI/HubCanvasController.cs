using UnityEngine;

public class HubCanvasController : BaseCanvasController
{
    [SerializeField] private UpgradesScreen _upgradesScreenPrefab;
    [SerializeField] private LoadoutScreen _loadoutScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Upgrades => Instantiate(_upgradesScreenPrefab, transform),
            GameScreenType.Loadout => Instantiate(_loadoutScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}

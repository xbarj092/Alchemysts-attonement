using UnityEngine;

public class HubCanvasController : BaseCanvasController
{
    [SerializeField] private UpgradesScreen _upgradesScreenPrefab;
    [SerializeField] private LoadoutScreen _loadoutScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Upgrades => Instantiate(_upgradesScreenPrefab, FindObjectOfType<Canvas>().transform),
            GameScreenType.Loadout => Instantiate(_loadoutScreenPrefab, FindObjectOfType<Canvas>().transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}

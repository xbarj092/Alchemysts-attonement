using UnityEngine;

public class HubCanvasController : BaseCanvasController
{
    [SerializeField] private UpgradesScreen _upgradesScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Upgrades => Instantiate(_upgradesScreenPrefab, FindObjectOfType<Canvas>().transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}

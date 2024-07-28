using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private PauseMenuScreen _pauseMenuScreenPrefab;
    [SerializeField] private OptionsScreen _optionsScreenPrefab;
    [SerializeField] private DeathScreen _deathScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Death => Instantiate(_deathScreenPrefab, transform),
            GameScreenType.Options => Instantiate(_optionsScreenPrefab, transform),
            GameScreenType.Pause => Instantiate(_pauseMenuScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }

    protected override GameScreen GetActiveGameScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Options => Instantiate(_pauseMenuScreenPrefab, transform),
            _ => base.GetActiveGameScreen(gameScreenType),
        };
    }
}

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
            GameScreenType.Death => Instantiate(_deathScreenPrefab, FindObjectOfType<Canvas>().transform),
            GameScreenType.Options => Instantiate(_optionsScreenPrefab, FindObjectOfType<Canvas>().transform),
            GameScreenType.Pause => Instantiate(_pauseMenuScreenPrefab, FindObjectOfType<Canvas>().transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}

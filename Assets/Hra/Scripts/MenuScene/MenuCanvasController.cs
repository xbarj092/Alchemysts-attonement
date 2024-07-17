using UnityEngine;

public class MenuCanvasController : BaseCanvasController
{
    [SerializeField] private MenuMainButtons _menuMainButtonsPrefab;
    [SerializeField] private OptionsScreen _optionsScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.MenuMain => Instantiate(_menuMainButtonsPrefab, FindObjectOfType<Canvas>().transform),
            GameScreenType.Options => Instantiate(_optionsScreenPrefab, FindObjectOfType<Canvas>().transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}

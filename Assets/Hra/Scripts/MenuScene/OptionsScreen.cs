public class OptionsScreen : GameScreen
{
    public void CloseScreen()
    {
        ScreenEvents.OnGameScreenClosedInvoke(_gameScreenType);
    }
}

using UnityEngine;

public class PauseMenuScreen : GameScreen
{
    public void Resume()
    {
        Close();
    }

    public void Options()
    {
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Options);
        ScreenEvents.OnGameScreenClosedInvoke(_gameScreenType);
    }

    public void ReturnToHub()
    {
        SceneLoadManager.Instance.GoGameToHub();
    }

    public void ReturnToMenu()
    {
        SceneLoadManager.Instance.GoGameToMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

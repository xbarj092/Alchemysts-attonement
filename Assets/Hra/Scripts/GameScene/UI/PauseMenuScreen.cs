using UnityEngine;

public class PauseMenuScreen : GameScreen
{
    public void Resume()
    {
        CloseScreen();
        GameManager.Instance.Paused = false;
    }

    public void Options()
    {
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Options);
        ScreenEvents.OnGameScreenClosedInvoke(GameScreenType);
    }

    public void ReturnToHub()
    {
        GameManager.Instance.Paused = false;
        SceneLoadManager.Instance.GoGameToHub();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.Paused = false;
        SceneLoadManager.Instance.GoGameToMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

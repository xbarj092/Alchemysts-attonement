using UnityEngine;

public class MenuMainButtons : GameScreen
{
    public void PlayTheGame()
    {
        SceneLoadManager.Instance.GoMenuToHub();
    }

    public void GoToOptions()
    {
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Options);
        CloseScreen();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

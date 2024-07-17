using UnityEngine;

public class DeathScreen : GameScreen
{
    public void Continue()
    {
        SceneLoadManager.Instance.GoGameToHub();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

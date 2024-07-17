using UnityEngine;

public class KeyboardInputHandler : IInputHandler
{
    public void HandleInput()
    {
        if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene))
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.Paused)
            {
                GameManager.Instance.Paused = true;
                ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ScreenManager.Instance.ActiveGameScreen != null)
            {
                ScreenManager.Instance.ActiveGameScreen.Close();
            }
        }
    }
}

using UnityEngine;

public class KeyboardInputHandler : IInputHandler
{
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
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

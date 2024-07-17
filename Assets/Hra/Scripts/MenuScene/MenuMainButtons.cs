using System;
using UnityEngine;

public class MenuMainButtons : MonoBehaviour
{
    public event Action<GameScreenType> OnOptionsOpened;

    public void PlayTheGame()
    {
        SceneLoadManager.Instance.GoMenuToHub();
    }

    public void GoToOptions()
    {
        OnOptionsOpened?.Invoke(GameScreenType.Options);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

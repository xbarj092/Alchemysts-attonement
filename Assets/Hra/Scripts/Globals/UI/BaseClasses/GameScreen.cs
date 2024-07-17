using UnityEngine;

public enum GameScreenType
{
    None = 0,
    Options = 1,
    Death = 2,
    Pause = 3,
    MenuMain = 4,
    Upgrades = 5,
    Loadout = 6
}

public class GameScreen : MonoBehaviour
{
    public GameScreenType GameScreenType;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        Destroy(gameObject);
    }

    public void CloseScreen()
    {
        ScreenEvents.OnGameScreenClosedInvoke(GameScreenType);
    }
}

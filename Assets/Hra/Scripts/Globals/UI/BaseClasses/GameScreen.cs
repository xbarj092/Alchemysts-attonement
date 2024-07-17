using UnityEngine;

public enum GameScreenType
{
    None = 0,
    Options = 1,
    Death = 2,
    Pause = 3,
    MenuMain = 4
}

public class GameScreen : MonoBehaviour
{
    [SerializeField] protected GameScreenType _gameScreenType;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        Destroy(gameObject);
    }
}

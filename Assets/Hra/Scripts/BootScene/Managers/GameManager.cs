public class GameManager : MonoSingleton<GameManager>
{
    private GameSceneManager _gameSceneManager = new();
    private HubSceneManager _hubSceneManager = new();

    public bool Paused;

    public void SetUpGameScene()
    {
        _gameSceneManager.SetUpGameScene();
    }

    public void SetUpHubScene()
    {
        _hubSceneManager.SetUpHubScene();
    }
}

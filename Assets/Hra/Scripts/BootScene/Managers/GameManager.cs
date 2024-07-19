public class GameManager : MonoSingleton<GameManager>
{
    private GameSceneManager _gameSceneManager = new();
    private HubSceneManager _hubSceneManager = new();

    public bool Paused;

    private void Start()
    {
        LocalDataStorage.Instance.PlayerData.PlayerStats = new(100, 100);
        LocalDataStorage.Instance.PlayerData.CurrencyData = new(99999, 0, 100);
    }

    public void SetUpGameScene()
    {
        _gameSceneManager.SetUpGameScene();
    }

    public void SetUpHubScene()
    {
        _hubSceneManager.SetUpHubScene();
    }
}

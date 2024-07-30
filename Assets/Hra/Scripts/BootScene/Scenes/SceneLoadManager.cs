public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    protected override void Init()
    {
        base.Init();
        GoBootToMenu();
    }

    public void GoBootToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnBootToMenuLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene);
    }

    private void OnBootToMenuLoadDone(SceneLoader.Scenes scene)
    {
        SceneLoader.OnSceneLoadDone -= OnBootToMenuLoadDone;
    }

    public void GoMenuToHub()
    {
        SceneLoader.OnSceneLoadDone += OnMenuToHubLoadDone;
        // Cursor.lockState = CursorLockMode.Locked;
        SceneLoader.LoadScene(SceneLoader.Scenes.HubScene, toUnload: SceneLoader.Scenes.MenuScene, 
            onSuccess: () => GameManager.Instance.SetUpHubScene());
    }

    private void OnMenuToHubLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnMenuToHubLoadDone;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoHubToGame()
    {
        SceneLoader.OnSceneLoadDone += OnHubToGameLoadDone;
        // Cursor.lockState = CursorLockMode.Locked;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.HubScene, 
            onSuccess: () => GameManager.Instance.SetUpGameScene());
    }

    private void OnHubToGameLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnHubToGameLoadDone;
    }

    public void GoGameToHub()
    {
        SceneLoader.OnSceneLoadDone += OnGameToHubLoadDone;
        // Cursor.lockState = CursorLockMode.Confined;
        SceneLoader.LoadScene(SceneLoader.Scenes.HubScene, toUnload: SceneLoader.Scenes.GameScene, 
            onSuccess: () => GameManager.Instance.SetUpHubScene());
    }

    private void OnGameToHubLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnGameToHubLoadDone;
    }

    public void GoGameToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnGameToMenuLoadDone;
        // Cursor.lockState = CursorLockMode.Confined;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene, toUnload: SceneLoader.Scenes.GameScene);
    }

    private void OnGameToMenuLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnGameToMenuLoadDone;
    }

    public void GoHubToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnHubToMenuLoadDone;
        // Cursor.lockState = CursorLockMode.Confined;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene, toUnload: SceneLoader.Scenes.HubScene);
    }

    private void OnHubToMenuLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnHubToMenuLoadDone;
    }

    public bool IsSceneLoaded(SceneLoader.Scenes sceneToCheck)
    {
        return SceneLoader.IsSceneLoaded(sceneToCheck);
    }
}

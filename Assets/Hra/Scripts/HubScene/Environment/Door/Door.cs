using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scenes _goToScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            switch (_goToScene)
            {
                case SceneLoader.Scenes.GameScene:
                    SceneLoadManager.Instance.GoHubToGame();
                    break;
                case SceneLoader.Scenes.MenuScene:
                    SceneLoadManager.Instance.GoHubToMenu();
                    break;
                default:
                    break;
            }
        }
    }
}

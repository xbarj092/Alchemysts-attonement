using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    [SerializeField] private CursorSpriteSwapper _cursorSpriteSwapper;

    private KeyboardInputHandler _keyboardInputHandler = new();
    private MouseInputHandler _mouseInputHandler;

    private void Awake()
    {
        _mouseInputHandler = new(_cursorSpriteSwapper);
    }

    private void Update()
    {
        _keyboardInputHandler.HandleInput();
        _mouseInputHandler.HandleInput();
    }
}

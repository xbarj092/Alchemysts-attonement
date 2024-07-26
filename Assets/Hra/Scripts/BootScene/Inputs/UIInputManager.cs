using System;
using UnityEngine;

public class UIInputManager : MonoSingleton<UIInputManager>
{
    [SerializeField] private CursorSpriteSwapper _cursorSpriteSwapper;

    private KeyboardInputHandler _keyboardInputHandler = new();
    private MouseInputHandler _mouseInputHandler;

    public event Action<string> OnToolTipSpawned;
    public event Action OnToolTipClosed;

    private void Awake()
    {
        _mouseInputHandler = new(_cursorSpriteSwapper);
    }

    private void OnEnable()
    {
        _mouseInputHandler.OnToolTipSpawned += OnToolTipSpawned;
        _mouseInputHandler.OnToolTipClosed += OnToolTipClosed;
    }

    private void OnDisable()
    {
        _mouseInputHandler.OnToolTipSpawned -= OnToolTipSpawned;
        _mouseInputHandler.OnToolTipClosed -= OnToolTipClosed;
    }

    private void Update()
    {
        _keyboardInputHandler.HandleInput();
        _mouseInputHandler.HandleInput();
    }
}

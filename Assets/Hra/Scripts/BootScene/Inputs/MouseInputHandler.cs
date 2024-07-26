using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseInputHandler : IInputHandler
{
    private CursorSpriteSwapper _cursorSpriteSwapper;
    private TooltipSpawner _tooltipSpawner;

    private Canvas _activeCanvas;
    private GraphicRaycaster _graphicRaycaster;

    public event Action<string> OnToolTipSpawned;
    public event Action OnToolTipClosed;

    public MouseInputHandler(CursorSpriteSwapper cursorSpriteSwapper)
    {
        _cursorSpriteSwapper = cursorSpriteSwapper;
    }

    public void HandleInput()
    {
        if (Camera.main != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HandleMouseClick(mousePosition);
            HandleMouseHover(mousePosition);
        }
    }

    private void HandleMouseClick(Vector2 mousePosition)
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                GameScreenType gameScreenType = GetGameScreenType(hit.collider.gameObject.tag);
                if (gameScreenType != GameScreenType.None)
                {
                    ScreenEvents.OnGameScreenOpenedInvoke(gameScreenType);
                }
            }
        }
    }

    private GameScreenType GetGameScreenType(string tag)
    {
        return tag switch
        {
            string t when t == GlobalConstants.Tags.Upgrades.ToString() => GameScreenType.Upgrades,
            string t when t == GlobalConstants.Tags.Loadout.ToString() => GameScreenType.Loadout,
            _ => GameScreenType.None,
        };
    }

    private void HandleMouseHover(Vector2 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (IsOnInteract(hit) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (IsValidTag(hit, out GlobalConstants.Tags tag))
            {
                _cursorSpriteSwapper.SetCursorToInteract(tag);
                return;
            }
        }

        List<RaycastResult> uiResults = RaycastToUI(mousePosition);
        foreach (RaycastResult result in uiResults)
        {
            //Debug.Log($"UI Hit: {result.gameObject.name}"); // Debugging line
            if (result.gameObject.CompareTag(GlobalConstants.Tags.ToolTip.ToString()))
            {
                string tooltip = _tooltipSpawner.SetToolTip(result);
                OnToolTipSpawned?.Invoke(tooltip);
                return;
            }
        }

        OnToolTipClosed?.Invoke();
        _cursorSpriteSwapper.ResetCursor();
    }

    private List<RaycastResult> RaycastToUI(Vector2 mousePosition)
    {
        PointerEventData pointerEventData = new(EventSystem.current)
        {
            position = mousePosition
        };

        List<RaycastResult> results = new();
        if (_activeCanvas == null)
        {
            _activeCanvas = GameObject.FindObjectOfType<Canvas>();
        }

        if (_graphicRaycaster == null)
        {
            _graphicRaycaster = _activeCanvas.GetComponent<GraphicRaycaster>();
        }

        _graphicRaycaster.Raycast(pointerEventData, results);

        return results;
    }

    private bool IsOnInteract(RaycastHit2D hit) => hit.collider != null && hit.transform.gameObject.layer == GlobalConstants.Layers.LAYER_INTERACT;
    private bool IsValidTag(RaycastHit2D hit, out GlobalConstants.Tags tag) => Enum.TryParse(hit.transform.tag, out tag);
}

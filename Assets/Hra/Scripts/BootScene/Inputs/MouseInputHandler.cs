using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputHandler : IInputHandler
{
    private CursorSpriteSwapper _cursorSpriteSwapper;

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

        _cursorSpriteSwapper.ResetCursor();
    }

    private bool IsOnInteract(RaycastHit2D hit) => hit.collider != null && hit.transform.gameObject.layer == GlobalConstants.Layers.LAYER_INTERACT;
    private bool IsValidTag(RaycastHit2D hit, out GlobalConstants.Tags tag) => Enum.TryParse(hit.transform.tag, out tag);
}

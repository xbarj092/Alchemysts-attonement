using System;
using UnityEngine;

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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            HandleMouseClick(ray);
            HandleMouseHover(ray);
        }
    }

    private void HandleMouseClick(Ray ray)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                
            }
        }
    }

    private void HandleMouseHover(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.layer == GlobalConstants.Layers.LAYER_INTERACT)
            {
                if (Enum.TryParse(hit.transform.tag, out GlobalConstants.Tags tag))
                {
                    _cursorSpriteSwapper.SetCursorToInteract(tag);
                }
                else
                {
                    _cursorSpriteSwapper.ResetCursor();
                }
            }
            else
            {
                _cursorSpriteSwapper.ResetCursor();
            }
        }
    }
}

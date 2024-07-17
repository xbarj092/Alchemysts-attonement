using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSpriteSwapper : MonoBehaviour
{
    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private SerializedDictionary<GlobalConstants.Tags, Texture2D> _cursorDictionary = new();

    public void SetCursorToInteract(GlobalConstants.Tags tag)
    {
        Cursor.SetCursor(_cursorDictionary[tag], Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(_defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}

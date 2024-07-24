using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [field: SerializeField] public List<Direction> Directions { get; set; } = new();

    [SerializeField] private List<RoomExit> _exits = new();

    public Vector2Int Coordinates { get; private set; }

    public event Action<Direction, Room> OnPlayerTrigger;

    private void OnEnable()
    {
        foreach (RoomExit exit in _exits)
        {
            exit.OnPlayerTrigger += MoveToNextRoom;
        }
    }

    private void OnDisable()
    {
        foreach (RoomExit exit in _exits)
        {
            exit.OnPlayerTrigger -= MoveToNextRoom;
        }
    }

    public void SetCoordinates(int x, int y)
    {
        Coordinates = new(x, y);
    }

    private void MoveToNextRoom(Direction direction)
    {
        OnPlayerTrigger?.Invoke(direction, this);
    }
}

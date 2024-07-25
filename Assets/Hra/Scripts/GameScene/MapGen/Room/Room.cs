using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialRoom
{
    None = 0,
    Start = 1,
    End = 2,
    MiniBoss = 3,
    Boss = 4
}

public class Room : MonoBehaviour
{
    [field: SerializeField] public Collider2D Confiner { get; private set; }
    [field: SerializeField] public List<Direction> Directions { get; set; } = new();
    [field: SerializeField] public SerializedDictionary<Direction, Transform> SpawnPositions = new();

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

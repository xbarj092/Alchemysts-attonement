using Cinemachine;
using System;
using UnityEngine;

public class RoomFactory
{
    private CinemachineConfiner2D _playerCamera;
    private MonoBehaviour _spawnObject;
    private Grid<GridNode> _grid;

    public event Action<Vector2> OnPlayerPositionReset;

    public RoomFactory(MonoBehaviour spawnObject, Grid<GridNode> grid)
    {
        _spawnObject = spawnObject;
        _grid = grid;

        GoToNextRoom(Direction.None, new(0, 0), SpecialRoom.Start);
    }

    private void MoveToNextRoom(Direction direction, Room room)
    {
        room.OnPlayerTrigger -= MoveToNextRoom;
        UnityEngine.Object.Destroy(room.gameObject);
        Vector2Int nextRoomCoordinates = GetNextRoomCoordinates(direction, room.Coordinates);
        GoToNextRoom(direction, nextRoomCoordinates);

        Debug.Log("[RoomPlacementHelper] - next room coordinates: " + nextRoomCoordinates);
    }

    private void GoToNextRoom(Direction direction, Vector2Int nextRoomCoordinates, SpecialRoom specialRoom = SpecialRoom.None)
    {
        GridNode nextNode = _grid.GetGridObject(nextRoomCoordinates.x, nextRoomCoordinates.y);
        if (nextNode != null && nextNode.Room != null)
        {
            Room instantiatedRoom = UnityEngine.Object.Instantiate(nextNode.Room, _spawnObject.transform);
            instantiatedRoom.SetCoordinates(nextRoomCoordinates.x, nextRoomCoordinates.y);
            instantiatedRoom.OnPlayerTrigger += MoveToNextRoom;
            if (specialRoom != SpecialRoom.None)
            {
                nextNode.SpecialRoom = specialRoom;
            }

            if (_playerCamera == null)
            {
                GameObject camera = GameObject.FindGameObjectWithTag(GlobalConstants.Tags.MainCamera.ToString());
                _playerCamera = camera.GetComponent<CinemachineConfiner2D>();
            }

            _playerCamera.m_BoundingShape2D = instantiatedRoom.Confiner;

            OnPlayerPositionReset?.Invoke(instantiatedRoom.SpawnPositions[direction].position);
        }
    }

    private Vector2Int GetNextRoomCoordinates(Direction direction, Vector2Int coordinates)
    {
        return direction switch
        {
            Direction.Left => new(coordinates.x - 1, coordinates.y),
            Direction.Right => new(coordinates.x + 1, coordinates.y),
            Direction.Up => new(coordinates.x, coordinates.y + 1),
            Direction.Down => new(coordinates.x, coordinates.y - 1),
            _ => Vector2Int.zero,
        };
    }
}

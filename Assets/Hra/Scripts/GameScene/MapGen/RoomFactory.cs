using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomFactory
{
    private CinemachineConfiner2D _playerCamera;
    private MonoBehaviour _spawnObject;
    private Grid<GridNode> _grid;

    private Dictionary<Vector2Int, RoomStatus> _roomStates = new();

    public event Action<Vector2> OnPlayerPositionReset;

    public RoomFactory(MonoBehaviour spawnObject, Grid<GridNode> grid)
    {
        _spawnObject = spawnObject;
        _grid = grid;

        GoToNextRoom(Direction.None, new(0, 0), SpecialRoom.Start);
    }

    private void MoveToNextRoom(Direction direction, Room room)
    {
        room.gameObject.SetActive(false);
        room.OnRoomDisabled -= SaveRoomProgress;
        room.OnPlayerTrigger -= MoveToNextRoom;
        UnityEngine.Object.Destroy(room.gameObject);
        Vector2Int nextRoomCoordinates = GetNextRoomCoordinates(direction, room.Status.Coordinates);
        GoToNextRoom(direction, nextRoomCoordinates);

        Debug.Log("[RoomPlacementHelper] - next room coordinates: " + nextRoomCoordinates);
    }

    private void GoToNextRoom(Direction direction, Vector2Int nextRoomCoordinates, SpecialRoom specialRoom = SpecialRoom.None)
    {
        GridNode nextNode = _grid.GetGridObject(nextRoomCoordinates.x, nextRoomCoordinates.y);
        if (nextNode != null && nextNode.Room != null)
        {
            Room instantiatedRoom = UnityEngine.Object.Instantiate(nextNode.Room, _spawnObject.transform);
            instantiatedRoom.Init(LoadRoomProgress(nextRoomCoordinates), nextRoomCoordinates);
            instantiatedRoom.OnPlayerTrigger += MoveToNextRoom;
            instantiatedRoom.OnRoomDisabled += SaveRoomProgress;
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

    private void SaveRoomProgress(RoomStatus roomStatus)
    {
        if (_roomStates.ContainsKey(roomStatus.Coordinates))
        {
            _roomStates.Remove(roomStatus.Coordinates);
        }

        _roomStates.Add(roomStatus.Coordinates, roomStatus);
    }

    private RoomStatus LoadRoomProgress(Vector2Int coordinates)
    {
        if (_roomStates.ContainsKey(coordinates))
        {
            return _roomStates[coordinates];
        }

        return null;
    }
}

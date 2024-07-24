using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomPlacementHelper
{
    private List<Room> _rooms = new();
    private MonoBehaviour _spawnObject;
    private Grid<GridNode> _grid;

    public RoomPlacementHelper(List<Room> rooms, MonoBehaviour spawnObject)
    {
        _rooms = rooms;
        _spawnObject = spawnObject;
    }

    public void PlaceRooms(Grid<GridNode> grid)
    {
        _grid = grid;

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PlaceRoom(grid.GetGridObject(x, y));
            }
        }
    }

    private void PlaceRoom(GridNode gridNode)
    {
        if (gridNode != null)
        {
            Room relevantRoom = GetRelevantRoom(gridNode.Walls);
            if (relevantRoom != null)
            {
                gridNode.Room = Object.Instantiate(relevantRoom, _spawnObject.transform);
                gridNode.Room.transform.position = _grid.GetWorldPosition(gridNode.X, gridNode.Y);
            }
        }
    }

    private Room GetRelevantRoom(List<Wall> walls)
    {
        List<Direction> allDirections = System.Enum.GetValues(typeof(Direction)).Cast<Direction>().Where(d => d != Direction.None).ToList();
        List<Direction> presentDirections = walls.Select(w => w.Direction).ToList();
        List<Direction> missingDirections = allDirections.Except(presentDirections).ToList();

        foreach (Room room in _rooms)
        {
            if (room.Directions.OrderBy(d => d).SequenceEqual(missingDirections.OrderBy(d => d)))
            {
                return room;
            }
        }

        return null;
    }
}

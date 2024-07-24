using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomPlacementHelper
{
    private List<Room> _rooms = new();
    private MonoBehaviour _spawnObject;

    public RoomPlacementHelper(List<Room> rooms, MonoBehaviour spawnObject)
    {
        _rooms = rooms;
        _spawnObject = spawnObject;
    }

    public void PlaceRooms(Grid<GridNode> grid)
    {
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
            }
        }
    }

    private Room GetRelevantRoom(List<Wall> walls)
    {
        var missingDirections = walls.Select(w => w.Direction).ToList();

        foreach (Room room in _rooms)
        {
            if (room.Directions.SequenceEqual(missingDirections))
            {
                return room;
            }
        }

        return null;
    }
}

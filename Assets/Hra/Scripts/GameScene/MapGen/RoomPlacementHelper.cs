using System;
using System.Collections.Generic;
using System.Linq;

public class RoomPlacementHelper
{
    private List<Room> _rooms = new();
    private Grid<GridNode> _grid;

    public RoomPlacementHelper(List<Room> rooms)
    {
        _rooms = rooms;
    }

    public void PlaceRooms(ref Grid<GridNode> grid)
    {
        _grid = grid;

        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                PlaceRoom(_grid.GetGridObject(x, y));
            }
        }

        grid = _grid;
    }

    private void PlaceRoom(GridNode gridNode)
    {
        if (gridNode != null)
        {
            Room relevantRoom = GetRelevantRoom(gridNode.Walls);
            if (relevantRoom != null)
            {
                gridNode.Room = relevantRoom;
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

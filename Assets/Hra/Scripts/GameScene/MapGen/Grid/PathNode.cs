using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class GridNode
{
    private Grid<GridNode> _grid;

    public int X;
    public int Y;

    public bool IsVisited;

    public Room Room;
    public GridNode CameFromNode;

    public List<Wall> Walls = new();

    public GridNode(Grid<GridNode> grid, int x, int y)
    {
        _grid = grid;
        X = x;
        Y = y;

        SetWalls();
    }

    private void SetWalls()
    {
        Walls.Add(new(Direction.Left));
        Walls.Add(new(Direction.Right));
        Walls.Add(new(Direction.Up));
        Walls.Add(new(Direction.Down));
    }

    public void ClearWall(Direction direction)
    {
        Wall wall = Walls.FirstOrDefault(wall => wall.Direction == direction);
        Walls.Remove(wall);
    }
}

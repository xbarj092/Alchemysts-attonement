using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DepthFirstSearchHelper
{
    private Grid<GridNode> _grid;

    private int _dungeonSizeX;
    private int _dungeonSizeY;

    public DepthFirstSearchHelper(Grid<GridNode> grid, int dungeonSizeX, int dungeonSizeY)
    {
        _grid = grid;
        _dungeonSizeX = dungeonSizeX;
        _dungeonSizeY = dungeonSizeY;
    }

    public void GenerateLevel(GridNode previousNode, GridNode currentNode)
    {
        currentNode.IsVisited = true;
        ClearWalls(previousNode, currentNode);
        GridNode nextNode;
        do
        {
            nextNode = GetNextUnvisitedNode(currentNode);
            if (nextNode != null)
            {
                GenerateLevel(currentNode, nextNode);
            }
        }
        while (nextNode != null);
    }

    private GridNode GetNextUnvisitedNode(GridNode currentNode)
    {
        IEnumerable<GridNode> unvisitedNodes = GetUnvisitedCells(currentNode);
        return unvisitedNodes.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<GridNode> GetUnvisitedCells(GridNode currentNode)
    {
        int[,] directions = new int[,]
        {
            { 1, 0 },
            { -1, 0 },
            { 0, 1 },
            { 0, -1 }
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newX = currentNode.X + directions[i, 0];
            int newY = currentNode.Y + directions[i, 1];

            if (newX >= 0 && newX < _dungeonSizeX && newY >= 0 && newY < _dungeonSizeY)
            {
                GridNode neighbor = _grid.GetGridObject(newX, newY);
                if (!neighbor.IsVisited)
                {
                    yield return neighbor;
                }
            }
        }
    }

    private void ClearWalls(GridNode previousNode, GridNode currentNode)
    {
        if (previousNode == null)
        {
            return;
        }

        if (previousNode.X < currentNode.X)
        {
            previousNode.ClearWall(Direction.Right);
            currentNode.ClearWall(Direction.Left);
        }
        else if (previousNode.X > currentNode.X)
        {
            previousNode.ClearWall(Direction.Left);
            currentNode.ClearWall(Direction.Right);
        }
        else if (previousNode.Y < currentNode.Y)
        {
            previousNode.ClearWall(Direction.Up);
            currentNode.ClearWall(Direction.Down);
        }
        else if (previousNode.Y > currentNode.Y)
        {
            previousNode.ClearWall(Direction.Down);
            currentNode.ClearWall(Direction.Up);
        }
    }
}

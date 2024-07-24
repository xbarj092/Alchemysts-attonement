using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;

    [SerializeField] private List<Room> _roomPrefabs;

    private Grid<GridNode> _grid;
    private DepthFirstSearchHelper _depthFirstSearchHelper;
    private RoomPlacementHelper _roomPlacementHelper;

    private const int DUNGEON_SIZE_X = 5;
    private const int DUNGEON_SIZE_Y = 5;

    private void Awake()
    {
        _grid = new Grid<GridNode>(DUNGEON_SIZE_X, DUNGEON_SIZE_Y, 20, (Grid<GridNode> g, int x, int y) => new GridNode(g, x, y));
        _depthFirstSearchHelper = new(_grid, DUNGEON_SIZE_X, DUNGEON_SIZE_Y);
        _roomPlacementHelper = new(_roomPrefabs, this);
    }

    private void Start()
    {
        _depthFirstSearchHelper.GenerateLevel(null, _grid.GetGridObject(0, 0));
        _roomPlacementHelper.PlaceRooms(_grid);

        // show visuals
        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                if (_grid.GetGridObject(x, y) != null)
                {
                    foreach (Wall wall in _grid.GetGridObject(x, y).Walls)
                    {
                        GameObject wallObject = _wallPrefab;

                        if (wall.Direction == Direction.Left)
                        {
                            wallObject.transform.position = _grid.GetWorldPosition(x, y) + new Vector3(-0.5f, 0, 0);
                            wallObject.transform.localScale = new Vector3(0.2f, 1, 0);
                        }
                        else if (wall.Direction == Direction.Right)
                        {
                            wallObject.transform.position = _grid.GetWorldPosition(x, y) + new Vector3(0.5f, 0, 0);
                            wallObject.transform.localScale = new Vector3(0.2f, 1, 0);
                        }
                        else if (wall.Direction == Direction.Up)
                        {
                            wallObject.transform.position = _grid.GetWorldPosition(x, y) + new Vector3(0, 0.5f, 0);
                            wallObject.transform.localScale = new Vector3(1, 0.2f, 0);
                        }
                        else
                        {
                            wallObject.transform.position = _grid.GetWorldPosition(x, y) + new Vector3(0, -0.5f, 0);
                            wallObject.transform.localScale = new Vector3(1, 0.2f, 0);
                        }

                        Instantiate(wallObject, transform);
                    }
                }
            }
        }
    }
}

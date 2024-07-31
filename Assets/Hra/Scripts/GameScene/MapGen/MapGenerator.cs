using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;

    [SerializeField] private List<Room> _roomPrefabs;

    private Grid<GridNode> _grid;
    private DepthFirstSearchHelper _depthFirstSearchHelper;
    private RoomPlacementHelper _roomPlacementHelper;
    private RoomFactory _roomFactory;

    private const int DUNGEON_SIZE_X = 6;
    private const int DUNGEON_SIZE_Y = 6;

    private void Awake()
    {
        _grid = new Grid<GridNode>(DUNGEON_SIZE_X, DUNGEON_SIZE_Y, 20, (Grid<GridNode> g, int x, int y) => new GridNode(g, x, y));
        _depthFirstSearchHelper = new(_grid, DUNGEON_SIZE_X, DUNGEON_SIZE_Y);
        _roomPlacementHelper = new(_roomPrefabs);
    }

    private void Start()
    {
        _depthFirstSearchHelper.GenerateLevel();
        _roomPlacementHelper.PlaceRooms(ref _grid);
        _roomFactory = new(this, _grid);
        _roomFactory.OnPlayerPositionReset += SpawnPlayer;

        SpawnPlayer(new(0, 0));
    }

    private void OnDisable()
    {
        _roomFactory.OnPlayerPositionReset -= SpawnPlayer;
    }

    private void SpawnPlayer(Vector2 position)
    {
        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                if (_grid.GetGridObject(x, y).Room != null)
                {
                    GameObject.FindGameObjectWithTag(GlobalConstants.Tags.Player.ToString()).transform.parent.position = position;
                }
            }
        }
    }
}

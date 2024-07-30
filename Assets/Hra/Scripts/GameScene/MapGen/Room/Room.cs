using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
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

    public int DistanceFromStart;
    public Vector2Int Coordinates { get; private set; }

    private float _enemyStatMultiplier;

    public event Action<Direction, Room> OnPlayerTrigger;

    private void OnEnable()
    {
        foreach (RoomExit exit in _exits)
        {
            exit.OnPlayerTrigger += MoveToNextRoom;
        }

        _enemyStatMultiplier = Mathf.Pow(LocalDataStorage.Instance.GameData.EnemyScaleData.RoomMultiplier, DistanceFromStart);
        StartCoroutine(SetEnemiesMultiplier());
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

    private IEnumerator SetEnemiesMultiplier()
    {
        yield return null;
        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.EnemyInstance.MaxHealth *= _enemyStatMultiplier;
            enemy.EnemyInstance.CurrentHealth = enemy.EnemyInstance.MaxHealth;
            enemy.EnemyInstance.Damage *= _enemyStatMultiplier / 2;
            enemy.EnemyInstance.DropsCoins *= _enemyStatMultiplier;
            enemy.EnemyInstance.DropsShadows *= _enemyStatMultiplier;
        }
    }
}

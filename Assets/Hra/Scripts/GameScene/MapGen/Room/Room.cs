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

    public RoomStatus Status;
    private float _enemyStatMultiplier;

    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Shadow _shadowPrefab;
    [SerializeField] private Enemy _enemyPrefab;

    public event Action<Direction, Room> OnPlayerTrigger;
    public event Action<RoomStatus> OnRoomDisabled;

    public void Init(RoomStatus status, Vector2Int coordinates)
    {
        Status = status;
        Status ??= new()
            {
                Coordinates = coordinates,
            };

        LoadProgress();

        _enemyStatMultiplier = Mathf.Pow(LocalDataStorage.Instance.GameData.EnemyScaleData.RoomMultiplier, Mathf.Abs(Status.Coordinates.x + Status.Coordinates.y));
        StartCoroutine(SetEnemiesMultiplier());
        
    }

    private void LoadProgress()
    {
        if (Status.CoinPositions != null && Status.CoinPositions.Count > 0)
        {
            foreach (Vector2 position in Status.CoinPositions)
            {
                Instantiate(_coinPrefab, position, Quaternion.identity, transform);
            }
        }

        if (Status.ShadowPositions != null && Status.ShadowPositions.Count > 0)
        {
            foreach (Vector2 position in Status.ShadowPositions)
            {
                Instantiate(_shadowPrefab, position, Quaternion.identity, transform);
            }
        }

        if (Status.EnemyStates != null && Status.EnemyStates.Count > 0)
        {
            foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
            {
                Destroy(enemy.gameObject);
            }

            foreach (EnemyStatus status in Status.EnemyStates)
            {
                StartCoroutine(InitEnemy(status));
            }
        }
    }

    private IEnumerator InitEnemy(EnemyStatus status)
    {
        Enemy enemy = Instantiate(_enemyPrefab, status.Position, Quaternion.identity, transform);
        yield return new WaitForSeconds(0.05f);
        enemy.EnemyInstance = status.EnemyInstance;
        enemy.UpdateHealth();
    }

    private void OnEnable()
    {
        foreach (RoomExit exit in _exits)
        {
            exit.OnPlayerTrigger += MoveToNextRoom;
        }
    }

    private void OnDisable()
    {
        foreach (RoomExit exit in _exits)
        {
            exit.OnPlayerTrigger -= MoveToNextRoom;
        }

        Vector2Int coordinates = Status.Coordinates;
        Status = new();
        Status.Coordinates = coordinates;
        foreach (Coin coin in GetComponentsInChildren<Coin>())
        {
            Status.CoinPositions.Add(coin.transform.position);
        }

        foreach (Shadow shadow in GetComponentsInChildren<Shadow>())
        {
            Status.ShadowPositions.Add(shadow.transform.position);
        }

        foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
        {
            EnemyStatus enemyStatus = new()
            {
                EnemyInstance = enemy.EnemyInstance,
                Position = enemy.transform.position
            };
            Status.EnemyStates.Add(enemyStatus);
        }

        if (Status.EnemyStates.Count == 0)
        {
            Status.IsCleared = true;
        }

        OnRoomDisabled?.Invoke(Status);
    }

    private void MoveToNextRoom(Direction direction)
    {
        OnPlayerTrigger?.Invoke(direction, this);
    }

    private IEnumerator SetEnemiesMultiplier()
    {
        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        if (Status.IsCleared)
        {
            foreach (Enemy enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }

            yield break;
        }

        yield return null;
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

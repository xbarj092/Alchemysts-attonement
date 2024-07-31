using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomStatus
{
    public Vector2Int Coordinates;
    public List<EnemyStatus> EnemyStates = new();
    public List<Vector2> CoinPositions = new();
    public List<Vector2> ShadowPositions = new();
    public bool IsCleared = false;
}

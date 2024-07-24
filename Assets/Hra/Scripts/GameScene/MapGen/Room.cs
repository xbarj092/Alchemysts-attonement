using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [field: SerializeField] public List<Direction> Directions { get; set; } = new();
}

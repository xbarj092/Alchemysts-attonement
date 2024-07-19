using UnityEngine;

public interface Imovable
{
    Rigidbody2D _rb { get; set; }
    void MoveEnemy(Vector2 velocity);
    void CheckforDirection(Vector2 velocity);
}

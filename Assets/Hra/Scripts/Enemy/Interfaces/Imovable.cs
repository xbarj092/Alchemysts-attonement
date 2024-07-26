using UnityEngine;

public interface IMovable
{
    Rigidbody2D Rb { get; set; }
    void MoveEnemy(Vector2 velocity);
    void CheckforDirection(Vector2 velocity);
}

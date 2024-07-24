using System;

public enum Direction
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 3,
    Down = 4,
}

[Serializable]
public class Wall
{
    public Direction Direction;

    public Wall(Direction direction)
    {
        Direction = direction;
    }
}

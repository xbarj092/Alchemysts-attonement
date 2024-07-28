using System.Collections;

public interface IPlayerEffect
{
    IEnumerator ApplyEffect(PlayerStats target, ElementItem element);
}

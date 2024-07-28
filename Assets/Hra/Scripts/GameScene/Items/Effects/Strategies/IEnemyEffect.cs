using System.Collections;

public interface IEnemyEffect
{
    IEnumerator ApplyEffect(Enemy target, ElementItem element);
}

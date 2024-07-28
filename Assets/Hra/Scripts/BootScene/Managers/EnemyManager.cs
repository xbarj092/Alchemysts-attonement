using System.Collections.Generic;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    private List<Enemy> _activeEnemies = new();

    public void RegisterEnemy(Enemy enemy)
    {
        _activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
    }

    public List<Enemy> GetAllEnemies()
    {
        return new(_activeEnemies);
    }
}

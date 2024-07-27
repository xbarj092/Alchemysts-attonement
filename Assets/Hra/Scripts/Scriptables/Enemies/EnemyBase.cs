using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Game/EnemyBase")]
public class EnemyBase : ScriptableObject
{
    public float Health;
    public float Damage;

    public int DropsCoins;
    public int DropsShadows;
}

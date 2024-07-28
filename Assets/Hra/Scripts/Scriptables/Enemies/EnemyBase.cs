using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Game/EnemyBase")]
public class EnemyBase : ScriptableObject
{
    public float Health;
    public float Damage;
    public float AttackRate;
    public float MovementSpeed;

    public int DropsCoins;
    public int DropsShadows;
}

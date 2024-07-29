using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Game/EnemyBase")]
public class EnemyBase : ScriptableObject
{
    public float Health;
    public WeaponItem Weapon;
    public float MovementSpeed;
    public float TouchDamage;

    public int DropsCoins;
    public int DropsShadows;
}

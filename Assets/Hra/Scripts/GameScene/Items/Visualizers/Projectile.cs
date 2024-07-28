using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _damage;
    private float _lifetime = 5f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage)
    {
        _damage = damage;
        Destroy(gameObject, _lifetime);
    }

    public void Launch(Vector2 direction)
    {
        _rb.velocity = direction * 10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageTarget(collision.transform);
            Destroy(gameObject);
        }
    }

    private void DamageTarget(Transform targetGameObject)
    {
        if (targetGameObject.TryGetComponent<Enemy>(out var enemy))
        {
            PlayOnHitVisual();
            enemy.Damage(_damage);
        }
    }

    private void PlayOnHitVisual()
    {
        // _weaponVFXHandler.SetEffect(WeaponVFX.Hit, true);
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private WeaponVFXHandler _weaponVFXHandler = new();
    private float _damage;
    private float _lifetime = 5f;
    private Entity _holder;

    public void Init(Entity holder, float damage)
    {
        _holder = holder;
        _damage = damage;
        Destroy(gameObject, _lifetime);
    }

    public void Launch(Vector2 direction)
    {
        _rb.velocity = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageTarget(collision.transform);
    }

    private void DamageTarget(Transform targetGameObject)
    {
        PlayOnHitVisual();
        if (_holder is PlayerController && targetGameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(_damage);
            Destroy(gameObject);
        }

        if (_holder is Enemy && targetGameObject.TryGetComponent(out PlayerController playerController))
        {
            playerController.Damage(_damage);
            Destroy(gameObject);
        }
    }

    private void PlayOnHitVisual()
    {
        _weaponVFXHandler.SetEffect(WeaponVFX.Hit, true);
    }
}

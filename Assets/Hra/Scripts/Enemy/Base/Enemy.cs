using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Entity, IDamageable, IMovable, ITriggerCheckable
{
    [SerializeField] private EnemyBase _enemyStats;
    public EnemyInstance EnemyInstance;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EnemyVFXHandler _enemyVFXHandler;
    [SerializeField] private BaseWeapon _weapon;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Shadow _shadowPrefab;
    [SerializeField] private LayerMask _collisionLayerMask;

    public EnemyAnimator Animator;
    [field: SerializeField] public Rigidbody2D Rb { get; set; }
    [field: SerializeField] public StateMachine StateMachine { get; set; }
    public EnemyStateIdle IdleState { get; set; }
    public EnemyStateRoam RoamingState { get; set; }
    public EnemyStateChase ChasingState { get; set; }
    public EnemyStateAttack AttackState { get; set; }
    public EnemyStateHit HitState { get; set; }
    public EnemyStateDeath DeathState { get; set; }

    public bool IsHit { get; set; }
    public bool IsDead { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinAttackRange { get; set; }

    private bool _isFreezeApplied;
    public bool IsFreezeApplied
    {
        get => _isFreezeApplied;
        set
        {
            _isFreezeApplied = value;
            _enemyVFXHandler.SetEffect(SpecialEffect.EnemySlow, value);
        }
    }

    private bool _isDoTApplied;
    public bool IsDoTApplied
    {
        get => _isDoTApplied;
        set
        {
            _isDoTApplied = value;
            _enemyVFXHandler.SetEffect(SpecialEffect.Dot, value);
        }
    }

    private bool _isChainApplied;
    public bool IsChainApplied
    {
        get => _isChainApplied;
        set
        {
            _isChainApplied = value;
            _enemyVFXHandler.SetEffect(SpecialEffect.ChainDamage, value);
        }
    }

    public float MovementRange = 5f;

    private void Awake()
    {
        _weapon.SetHolder(this);
        EnemyInstance = new(_enemyStats);
        IdleState = new EnemyStateIdle(this, StateMachine);
        RoamingState = new EnemyStateRoam(this, StateMachine);
        ChasingState = new EnemyStateChase(this, StateMachine);
        AttackState = new EnemyStateAttack(this, StateMachine);
        HitState = new EnemyStateHit(this, StateMachine);
        DeathState = new EnemyStateDeath(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(RoamingState); //TODO: change to idle, this is for debugging
    }

    private void OnEnable()
    {
        EnemyManager.Instance.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        EnemyManager.Instance.UnregisterEnemy(this);
    }

    public void UseWeapon()
    {
        if (_weapon is BaseProjectile weapon)
        {
            weapon.Use();
        }
    }

    public void Damage(float damageAmount)
    {
        if (EnemyInstance.CurrentHealth <= 0)
        {
            return;
        }

        IsHit = true;
        EnemyInstance.CurrentHealth -= damageAmount;
        _healthBar.SetHealth(EnemyInstance.CurrentHealth, EnemyInstance.MaxHealth);

        if (EnemyInstance.CurrentHealth <= 0f)
        {
            _healthBar.enabled = false;
            Die();
        }
    }

    public void Die()
    {
        IsFreezeApplied = false;
        IsDoTApplied = false;
        IsChainApplied = false;

        for (int i = 0; i < EnemyInstance.DropsCoins; i++)
        {
            if ((i + 1) % 10 == 0)
            {
                Vector2 coinPosition = GetRandomPosition(transform.position, 1f);
                if (Physics2D.OverlapPoint(coinPosition, _collisionLayerMask))
                {
                    coinPosition = transform.position;
                }

                Coin coin = Instantiate(_coinPrefab, transform.parent).GetComponent<Coin>();
                coin.transform.position = coinPosition;
                coin.Init(10);
            }
        }

        for (int i = 0; i < EnemyInstance.DropsShadows; i++)
        {
            if ((i + 1) % 10 == 0)
            {
                Vector2 shadowPosition = GetRandomPosition(transform.position, 1f);
                if (Physics2D.OverlapPoint(shadowPosition, _collisionLayerMask))
                {
                    shadowPosition = transform.position;
                }

                Shadow shadow = Instantiate(_shadowPrefab, transform.parent).GetComponent<Shadow>();
                shadow.transform.position = shadowPosition;
                shadow.Init(10);
            }
        }

        Destroy(gameObject);
    }

    private Vector3 GetRandomPosition(Vector3 origin, float range)
    {
        float randomX = Random.Range(-range, range);
        float randomY = Random.Range(-range, range);
        return new Vector3(origin.x + randomX, origin.y + randomY, origin.z);
    }

    public void MoveEnemy(Vector2 velocity)
    {
        CheckforDirection(velocity);
        Rb.velocity = velocity;
    }

    public void CheckforDirection(Vector2 velocity)
    {
        // Implement direction checking logic
    }

    public void SetAggro(bool aggro)
    {
        IsAggroed = aggro;
    }

    public void SetWithinAttackRange(bool attackRangeCheck)
    {
        IsWithinAttackRange = attackRangeCheck;
    }
}

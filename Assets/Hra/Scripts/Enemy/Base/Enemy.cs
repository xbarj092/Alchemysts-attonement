using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Entity, IDamageable, IMovable, ITriggerCheckable
{
    [SerializeField] private EnemyBase _enemyStats;
    public EnemyInstance EnemyInstance;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EnemyVFXHandler _enemyVFXHandler;
    [SerializeField] private BaseWeapon _weapon; 

    public EnemyAnimator Animator;
    [field: SerializeField] public Rigidbody2D Rb { get; set; }
    [field: SerializeField] public EnemyStateMachine StateMachine { get; set; }
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
        Destroy(gameObject);
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

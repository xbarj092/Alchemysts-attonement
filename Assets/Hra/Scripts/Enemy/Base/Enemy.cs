using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable, IMovable, ITriggerCheckable
{
    [SerializeField] private EnemyBase _enemyStats;
    public EnemyInstance EnemyInstance;
    [SerializeField] private HealthBar _healthBar;

    public Rigidbody2D Rb { get; set; }

    [field: SerializeField] public EnemyAnimator Animator;
    [field: SerializeField] public EnemyStateMachine StateMachine { get; set; }
    public EnemyStateIdle IdleState {  get; set; }
    public EnemyStateRoam RoamingState { get; set; }
    public EnemyStateChase ChasingState { get; set; }
    public EnemyStateAttack AttackState { get; set; }
    public EnemyStateHit HitState { get; set; }
    public EnemyStateDeath DeathState { get; set; }

    public bool IsHit { get; set; }
    public bool IsDead { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithingAttackRange { get; set; }

    public float MovementRange = 5f;

    private void Awake()
    {
        EnemyInstance = new(_enemyStats);
        IdleState = new EnemyStateIdle(this, StateMachine);
        RoamingState = new EnemyStateRoam(this, StateMachine);
        ChasingState = new EnemyStateChase(this, StateMachine);
        AttackState = new EnemyStateAttack(this, StateMachine);
    }

    private void Start() 
    {
        Rb = GetComponent<Rigidbody2D>();

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
        Destroy(gameObject);
    }

    public void MoveEnemy(Vector2 velocity)
    {
        CheckforDirection(velocity);
        Rb.velocity = velocity;
    }

    public void CheckforDirection(Vector2 velocity)
    {

    }

    public void SetAggro(bool Aggro)
    {
        IsAggroed = Aggro;
    }

    public void SetWithinAttackRange(bool attackRangeCheck)
    {
        IsWithingAttackRange = attackRangeCheck;
    }
}

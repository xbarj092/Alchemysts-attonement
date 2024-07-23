using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable, Imovable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    [field: SerializeField] public float CurrentHealth { get; set; }
    public Rigidbody2D _rb { get; set; }

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyStateIdle IdleState {  get; set; }
    public EnemyStateRoam RoamingState { get; set; }
    public EnemyStateChase ChasingState { get; set; }
    public EnemyStateAttack AttackState { get; set; }
    public bool isAggroed { get; set; }
    public bool isWithingAttackRange { get; set; }

    #endregion

    #region Roam Variables

    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;

    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyStateIdle(this, StateMachine);
        RoamingState = new EnemyStateRoam(this, StateMachine);
        ChasingState = new EnemyStateChase(this, StateMachine);
        AttackState = new EnemyStateAttack(this, StateMachine);
    }

    private void Start() 
    {
        CurrentHealth = MaxHealth;

        _rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(RoamingState); //TODO: change to idle, this is for debugging
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    #region Health / Damage functions

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this);
    }

    #endregion

    #region Movement functions
    public void MoveEnemy(Vector2 velocity)
    {
        CheckforDirection(velocity);
        _rb.velocity = velocity;
    }

    public void CheckforDirection(Vector2 velocity)
    {

    }

    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTrigger trigger)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(trigger); // For animation events in the animator
    }

    public enum AnimationTrigger
    {
        EnemyIdle,
        EnemyRoaming,
        EnemyDamaged,
        EnemyAttacking,
        PlaySounds
    }

    #endregion

    #region Trigger Checks

    public void SetAggro(bool Aggro)
    {
        isAggroed = Aggro;
    }

    public void setwithinAttackRange(bool AttackRangeCheck)
    {
        isWithingAttackRange = AttackRangeCheck;
    }

    #endregion
}

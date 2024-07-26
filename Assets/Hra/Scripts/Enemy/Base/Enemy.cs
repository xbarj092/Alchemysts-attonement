using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable, IMovable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    [field: SerializeField] public float CurrentHealth { get; set; }
    public Rigidbody2D Rb { get; set; }

    #region State Machine Variables

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

    #endregion

    #region Roam Variables

    public float MovementRange = 5f;
    public float MovementSpeed = 1f;

    #endregion

    private void Awake()
    {
        IdleState = new EnemyStateIdle(this, StateMachine);
        RoamingState = new EnemyStateRoam(this, StateMachine);
        ChasingState = new EnemyStateChase(this, StateMachine);
        AttackState = new EnemyStateAttack(this, StateMachine);
    }

    private void Start() 
    {
        CurrentHealth = MaxHealth;

        Rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(RoamingState); //TODO: change to idle, this is for debugging
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
        Rb.velocity = velocity;
    }

    public void CheckforDirection(Vector2 velocity)
    {

    }

    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTrigger trigger)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(trigger);
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
        IsAggroed = Aggro;
    }

    public void SetWithinAttackRange(bool attackRangeCheck)
    {
        IsWithingAttackRange = attackRangeCheck;
    }

    #endregion
}

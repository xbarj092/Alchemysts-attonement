using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Entity
{
    [Header("BASICS")]
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private PlayerWeapons _playerWeapons;

    private Animator _weaponAnimator;
    Vector2 _mousePosition;

    [Header("MOVEMENT")]
    [SerializeField] private float rotationDampingSpeed = 5f;
    [SerializeField] private float _movementSpeed = 8f;

    [Header("DASHING")]
    [SerializeField] private float _dashForce = 10f;
    [SerializeField] private float _dashDuration = 0.25f;
    [SerializeField] private float _dashCooldown = 1f;
    private bool CanDash = false;
    private bool isDashing = false;

    public bool CanMove = false;

    private float _slowTimeDuration = 0.1f;
    private float _slowTimeScale = 0.1f;
    private float _launchForce = 50f;

    private bool _invulnerable = false;

    public bool IsHit = false;
    public bool IsDead = false;

    public PlayerAnimator Animator;
    [field: SerializeField] public StateMachine StateMachine;
    public PlayerIdleState IdleState;
    public PlayerDashState DashState;
    public PlayerMoveState MoveState;
    public PlayerAttackState AttackState;
    public PlayerHitState HitState;
    public PlayerDeathState DeathState;

    Camera cameraMain;

    private BaseWeapon _weapon;

    public bool CanAttack = true;
    public bool IsAttacking = false;

    public bool HasElementsOn = false;

    private const float ELEMENT_ON_TIME = 5f;
    private const float INVLULNERABLE_TIME = 1f;

    private void Awake()
    {
        if (TutorialManager.Instance.CompletedTutorials.Contains(TutorialID.Shop))
        {
            CanDash = true;
            CanMove = true;
        }

        IdleState = new(this, StateMachine);
        DashState = new(this, StateMachine);
        MoveState = new(this, StateMachine);
        AttackState = new(this, StateMachine);
        HitState = new(this, StateMachine);
        DeathState = new(this, StateMachine);
        StateMachine.Initialize(IdleState);

        cameraMain = Camera.main;
        _weaponAnimator = _playerWeapons.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerWeapons.OnWeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _playerWeapons.OnWeaponChanged -= OnWeaponChanged;
    }

    private void Update()
    {
        if (isDashing) return;
        
        GetInputs();
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        AdjustViewDirection();
    }

    private void OnWeaponChanged(BaseWeapon newWeapon)
    {
        _weapon = newWeapon;
        _weapon.SetHolder(this);
        _weaponAnimator = _playerWeapons.GetComponentInChildren<Animator>();
    }

    private void GetInputs()
    {
        _mousePosition = cameraMain.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && CanDash)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsAttacking && _weaponAnimator != null)
        {
            StartCoroutine(Attack());
        }

        CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
        if (Input.GetKeyDown(KeyCode.Mouse1) && currencyData.CurrentShadows == currencyData.MaxShadows && !HasElementsOn)
        {
            StartCoroutine(TriggerAbilities());
        }
    }

    private IEnumerator TriggerAbilities()
    {
        HasElementsOn = true;
        float elapsedTime = 0f;
        CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
        float maxShadows = currencyData.MaxShadows;

        while (elapsedTime < ELEMENT_ON_TIME)
        {
            elapsedTime += Time.deltaTime;
            float proportion = 1 - (elapsedTime / ELEMENT_ON_TIME);
            currencyData.CurrentShadows = maxShadows * proportion;
            LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
            yield return null;
        }

        currencyData.CurrentShadows = 0f;
        LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
        HasElementsOn = false;
    }

    private IEnumerator Attack()
    {
        if (_weapon is MeleeWeapon melee)
        {
            melee.Use();
        }

        _weaponAnimator.speed = LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate;
        _weaponAnimator.SetTrigger("Attack");
        IsAttacking = true;
        yield return new WaitForSeconds(1 / LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate);
        IsAttacking = false;
    }

    private void AdjustViewDirection()
    {
        Vector2 aimDirection = _mousePosition - _rigidBody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

        if (_weapon != null && _weapon.State != WeaponStates.Using)
        {
        }
        _playerWeapons.transform.rotation = Quaternion.Euler(0f, 0f, aimAngle + 90f);
    }

    public void Damage(float damageAmount)
    {
        if (_invulnerable)
        {
            return;
        }

        IsHit = true;
        PlayerStats playerStats = LocalDataStorage.Instance.PlayerData.PlayerStats;
        playerStats.CurrentHealth -= damageAmount;
        LocalDataStorage.Instance.PlayerData.PlayerStats = playerStats;

        if (playerStats.CurrentHealth <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        IsDead = true;
        _playerWeapons.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        PlayerStats playerStats = LocalDataStorage.Instance.PlayerData.PlayerStats;
        playerStats.CurrentHealth = playerStats.MaxHealth;
        CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
        currencyData.CurrentShadows = 0;
        LocalDataStorage.Instance.PlayerData.PlayerStats = playerStats;
        LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Death);
    }

    public void Heal(float amount)
    {

    }

    public void Move(Vector2 movement)
    {
        Debug.Log("[PlayerController] - moving");
        _rigidBody.velocity = movement.normalized * _movementSpeed;
        HandleAnimator(_rigidBody.velocity);
    }

    private void HandleAnimator(Vector2 velocity)
    {
        if (velocity.x > 0) 
        {
            Animator.PlayAnimation(PlayerAnimationTrigger.PlayerMoveRight);
            return;
        }
        else if (velocity.x < 0)
        {
            Animator.PlayAnimation(PlayerAnimationTrigger.PlayerMoveLeft);
            return;
        }
        else if(velocity.y > 0)
        {
            Animator.PlayAnimation(PlayerAnimationTrigger.PlayerMoveUp);
            return;
        }
        else if (velocity.y < 0)
        {
            Animator.PlayAnimation(PlayerAnimationTrigger.PlayerMoveDown);
            return;
        }
        else
        {
            Animator.PlayAnimation(PlayerAnimationTrigger.PlayerIdle);
            return;
        }

    }

    private IEnumerator Dash()
    {
        CanDash = false;
        isDashing = true;
        Vector2 originalVelocity = _rigidBody.velocity;
        // _rigidBody.velocity = _movement.normalized * _dashForce;
        yield return new WaitForSeconds(_dashDuration);
        _rigidBody.velocity = originalVelocity;
        isDashing = false;
        yield return new WaitForSeconds(_dashCooldown);
        CanDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_invulnerable && collision.gameObject.CompareTag(GlobalConstants.Tags.Enemy.ToString()))
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                Damage(enemy.EnemyInstance.TouchDamage);
                StartCoroutine(SetInvulnerable());
                StartCoroutine(SlowTime());
                LaunchEnemy(enemy);
            }
        }
    }

    private IEnumerator SetInvulnerable()
    {
        _invulnerable = true;
        yield return new WaitForSecondsRealtime(INVLULNERABLE_TIME);
        _invulnerable = false;
    }

    private IEnumerator SlowTime()
    {
        Time.timeScale = _slowTimeScale;
        yield return new WaitForSecondsRealtime(_slowTimeDuration);
        Time.timeScale = 1f;
    }

    private void LaunchEnemy(Enemy enemy)
    {
        if (enemy.TryGetComponent(out Rigidbody2D rigidbody))
        {
            Vector2 launchDirection = (enemy.transform.position - transform.position).normalized;
            rigidbody.AddForce(launchDirection * _launchForce, ForceMode2D.Impulse);
        }
    }
}

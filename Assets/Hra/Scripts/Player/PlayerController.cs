using System.Collections;
using UnityEngine;

public class PlayerController : Entity
{
    [Header("BASICS")]
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private PlayerWeapons _playerWeapons;

    private Animator _animator;
    Vector2 _mousePosition;
    GameObject Head;

    [Header("MOVEMENT")]
    [SerializeField] private float _movementSpeed = 8f;

    [Header("DASHING")]
    [SerializeField] private float _dashForce = 20f;
    [SerializeField] private float _dashDuration = 0.25f;
    [SerializeField] private float _dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;

    private Vector2 _movement;
    Camera cameraMain;

    private BaseWeapon _weapon;

    public bool CanAttack = true;
    public bool IsAttacking = false;

    private void Awake()
    {
        cameraMain = Camera.main;
        _animator = _playerWeapons.GetComponent<Animator>();
        Head = gameObject.transform.GetChild(0).gameObject;
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

        Move();
        AdjustViewDirection();
    }

    private void OnWeaponChanged(BaseWeapon newWeapon)
    {
        _weapon = newWeapon;
        _weapon.SetHolder(this);
        _animator = _playerWeapons.GetComponentInChildren<Animator>();
    }

    private void GetInputs()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        _mousePosition = cameraMain.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsAttacking && _animator != null)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        if (_weapon is MeleeWeapon melee)
        {
            melee.Use();
        }

        _animator.speed = LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate;
        _animator.SetTrigger("Attack");
        IsAttacking = true;
        yield return new WaitForSeconds(1 / LocalDataStorage.Instance.PlayerData.LoadoutData.WeaponInstance.AttackRate);
        IsAttacking = false;
    }

    private void AdjustViewDirection()
    {
        Vector2 aimDirection = _mousePosition - _rigidBody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        Head.transform.rotation = Quaternion.Euler(0f, 0f, aimAngle);
        _playerWeapons.transform.rotation = Quaternion.Euler(0f, 0f, aimAngle+90f);
    }

    public void Damage(float damageAmount)
    {
        PlayerStats playerStats = LocalDataStorage.Instance.PlayerData.PlayerStats;
        playerStats.CurrentHealth -= damageAmount;
        LocalDataStorage.Instance.PlayerData.PlayerStats = playerStats;

        if (playerStats.CurrentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Death);
    }

    public void Heal(float amount)
    {

    }

    private void Move()
    {
        _rigidBody.velocity = _movement.normalized * _movementSpeed;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Vector2 originalVelocity = _rigidBody.velocity;
        _rigidBody.velocity = _movement.normalized * _dashForce;
        yield return new WaitForSeconds(_dashDuration);
        _rigidBody.velocity = originalVelocity;
        isDashing = false;
        yield return new WaitForSeconds(_dashCooldown);
        canDash = true;
    }
}

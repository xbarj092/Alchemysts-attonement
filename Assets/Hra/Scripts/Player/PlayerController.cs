using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("BASICS")]
    [SerializeField] private Rigidbody2D _rigidBody;
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

    private PlayerWeapon playerWeapon;

    private void Awake()
    {
        cameraMain = Camera.main;
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
        Head = gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (isDashing) return;
        
        getInputs();
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        Move();
        adjustViewDirection();
    }


    private void getInputs()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var animator = playerWeapon.GetComponentInChildren<Animator>();

        _mousePosition = cameraMain.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && animator != null)
        {
            animator.SetTrigger("Attack");
        }

    }

    private void adjustViewDirection()
    {
        Vector2 aimDirection = _mousePosition - _rigidBody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        Head.transform.rotation = Quaternion.Euler(0f, 0f, aimAngle);
        playerWeapon.transform.rotation = Quaternion.Euler(0f, 0f, aimAngle+90f);
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

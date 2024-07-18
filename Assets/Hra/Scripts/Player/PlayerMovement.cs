using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("BASICS")]
    [SerializeField] private Rigidbody2D _rb;
    Vector2 _mousePosition;

    [Header("MOVEMENT")]
    [SerializeField] private float _movementSpeed = 8f;

    [Header("DASHING")]
    [SerializeField] private float _dashForce = 20f;
    [SerializeField] private float _dashDuration = 0.25f;
    [SerializeField] private float _dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;

    private Vector2 _movement;

    private void Update()
    {
        if (isDashing) return;
        
        getInputs();
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        adjustViewDirection();
        Move();
    }


    private void getInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        _movement = new Vector2(moveX, moveY).normalized;

        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private void adjustViewDirection()
    {
        Vector2 aimDirection = _mousePosition - _rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = aimAngle;
    }
    private void Move()
    {
        _rb.velocity = _movement.normalized * _movementSpeed;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Vector2 originalVelocity = _rb.velocity;
        _rb.velocity = _movement.normalized * _dashForce;
        yield return new WaitForSeconds(_dashDuration);
        _rb.velocity = originalVelocity;
        isDashing = false;
        yield return new WaitForSeconds(_dashCooldown);
        canDash = true;
    }
}

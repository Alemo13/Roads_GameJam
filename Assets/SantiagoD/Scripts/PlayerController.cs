using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [Header("Data")]
    [SerializeField] private float health = 100f;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private float decceleration = 0.2f;
    [SerializeField] private float velPower = 1f;
    private bool isFacingRight = true;
    private float moveInput;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 5f;

    private void Update()
    {
        GetInput();
        FlipPlayer();
        Jump();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    #region Movement
    private void GetInput()
    {
        moveInput = Input.GetAxis("Horizontal");
    }

    private void PlayerMovement()
    {
        float targetSpeed = moveInput * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelerationRatio = (Mathf.Abs(speedDif) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelerationRatio, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    
    private void FlipPlayer()
    {
        if(isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    #endregion

    private void Attack()
    {

    }
}

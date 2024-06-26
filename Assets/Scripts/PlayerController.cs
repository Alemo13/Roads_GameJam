using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    Animator animator;
    Damageable damageable;

    [Header("Data")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float damageCooldown = 2f;
    private float lastDamageTime;
    private bool isDeath = false;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private float decceleration = 0.2f;
    [SerializeField] private float velPower = 1f;
    private bool isFacingRight = true;
    private float moveInput;
    private bool canMove = true;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 5f;

    [Header("Attack")]
    [SerializeField] private EnemyInRange attackRange;

    [Header("Animator")]
    [SerializeField] private Animator playerAnimator;

    [Header("Sound")]
    [SerializeField] private AudioSource playerMoveAudioSource;
    [SerializeField] private AudioSource playerAttackAudioSource;

    [Header("Enemies")]
    private GameObject enemyInRange;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    private void Update()
    {
        if (!isDeath)
        {
            GetInput();
            FlipPlayer();
            Jump();
            ChangePlayerAnimations();
            ChangeSoundState();
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity && !isDeath)
        {
            PlayerMovement();
        }
    }

    #region Movement
    private void GetInput()
    {
        moveInput = Input.GetAxis("Horizontal");
    }

    private void PlayerMovement()
    {
        if(canMove) 
        {
            float targetSpeed = moveInput * speed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelerationRatio = (Mathf.Abs(speedDif) > 0.01f) ? acceleration : decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelerationRatio, velPower) * Mathf.Sign(speedDif);

            rb.AddForce(movement * Vector2.right);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ChangePlayerAnimations()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }

        playerAnimator.SetFloat("yVelocity", rb.velocity.y);

        canMove = playerAnimator.GetBool("canMove");

        playerAnimator.SetBool("isGrounded", IsGrounded());
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() && canMove)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerAnimator.SetTrigger("jump");
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

    private void ChangeSoundState()
    {
        if(moveInput < 0f && canMove && IsGrounded() || moveInput > 0f && canMove && IsGrounded())
        {
            playerMoveAudioSource.enabled = true;
        }
        else
        {
            playerMoveAudioSource.enabled = false;
        }
    }
    #endregion

    #region Actions
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsGrounded())
        {
            playerAnimator.SetTrigger("hasAttacked");
            playerAttackAudioSource.PlayOneShot(playerAttackAudioSource.clip);
            //if (attackRange.ReturnEnemyInRange() != null)
            //{
            //    llamar recibir da�o enemigo
            //}
            
        }
    }

    public void GetDamaged(float damage)
    {
        if (!isDeath && Time.time - lastDamageTime > damageCooldown)
        {
            health -= damage;
            Debug.Log("Vida: " + health);
            lastDamageTime = Time.time;

            if (health < 0)
            {
                isDeath = true;
                playerAnimator.SetBool("isDeath", true);
            }
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    #endregion
}

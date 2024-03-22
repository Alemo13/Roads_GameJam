using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class EnemyManager : MonoBehaviour
{
    public EnemySO enemy;
    private Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    public DetectionZone attackZone;
    public DetectionZone cliffZone;
    public float walkStopRate = 0.001f;
    Animator animator;

    public enum WalkableDirection { Left, Right }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.left;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            if(_walkDirection != value)
            {
                if(value == WalkableDirection.Left) 
                {
                    walkDirectionVector = Vector2.left;
                }
                else if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
            }
            _walkDirection = value; }
    }

    public bool _hasTarget = false;
    public bool HasTarget { 
        get { return _hasTarget; } 
        private set 
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        } }

    public bool CanMove { 
        get 
        {
            return animator.GetBool(AnimationStrings.canMove); 
        } 
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if(CanMove)
            rb.velocity = new Vector2(enemy.walkSpeed * walkDirectionVector.x, rb.velocity.y);
        else 
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left) 
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Walkable direction is not set to a legal value");
        }
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
            FlipDirection();
    }
}

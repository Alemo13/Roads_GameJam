using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health = 100;
    [SerializeField] private bool _isAlive = true;


    Animator animator;
    [SerializeField] private bool isInvincible = false;
    private float timeSinceHit = 0f;
    public float invincibleTime = 0.25f;

    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set 
        {
            _maxHealth = value; 
        }
    } 
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public bool IsAlive { 
        get
        {
            return _isAlive;
        }
        set 
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        } 
    }
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isInvincible) 
        {
            if(timeSinceHit > invincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knocback)
    {
        if( IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knocback);
            return true;
        }
        return false;
    }
}

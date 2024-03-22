using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health = 100;
    [SerializeField] private bool _isAlive = true;

    Animator animator;
    [SerializeField] private bool isInvincible = false;
    private float timeSinceHit = 0f;
    public float invincibleTime = 0.25f;

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
            if(_health <= 0)
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

    public void Hit(int damage)
    {
        if( IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }
}
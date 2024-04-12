using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDefeated : MonoBehaviour
{
    [SerializeField] private int enemiesInScene = 0;

    [SerializeField] private UnityEvent onEnemiesExterminated;

    public void EnemyDeath()
    {
        enemiesInScene--;
        //Debug.Log("Enemigos restantes: " + enemiesInScene);
        if(enemiesInScene <= 0)
        {
            onEnemiesExterminated.Invoke();
        }
    }
}

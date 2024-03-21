using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    private GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("enemy"))
        {
            enemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("enemy"))
        {
            enemy = null;
        }
    }

    public GameObject ReturnEnemyInRange()
    {
        if(enemy != null)
        {
            return enemy;
        }
        return null;
    }
}

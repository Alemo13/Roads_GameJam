using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    public bool isFinalScene = false;
    [SerializeField] private UnityEvent onDoorEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onDoorEntered != null) { onDoorEntered.Invoke(); }
        if (!isFinalScene)
        {
            GameManager.Instance.NextScene();
        }
        else
        {
            RoadsChosed.Instance.CheckFinal();
        }

    }

    public void GoodDoor()
    {
        RoadsChosed.Instance.CollectedItemDoor();
    }
    public void BadDoor()
    {
        RoadsChosed.Instance.EnemiesDefeatedDoor();
    }
}

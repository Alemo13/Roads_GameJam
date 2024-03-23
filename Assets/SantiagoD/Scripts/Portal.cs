using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    [SerializeField] private UnityEvent onDoorEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onDoorEntered != null) {  onDoorEntered.Invoke(); }
        GameManager.Instance.NextScene();
    }
}

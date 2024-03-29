using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private Subject subject = new Subject();

    private void Start()
    {
        CollectionablesManager manager = FindObjectOfType<CollectionablesManager>();
        subject.Attach(manager);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            subject.Notify();
            gameObject.SetActive(false);
        }
    }
}

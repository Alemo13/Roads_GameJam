using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectionablesManager : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject finalDoor;
    [SerializeField] private int numberOfItems = 0;

    public void Notify()
    {
        CheckAllItems();
    }

    private void CheckAllItems()
    {
        numberOfItems--;
        if (numberOfItems <= 0) 
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        finalDoor.SetActive(true);
    }
}

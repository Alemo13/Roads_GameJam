using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectionablesManager : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject finalDoor;

    public void Notify()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        finalDoor.SetActive(true);
    }
}

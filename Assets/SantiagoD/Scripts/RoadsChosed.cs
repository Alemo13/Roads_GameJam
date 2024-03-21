using System.Collections.Generic;
using UnityEngine;

public class RoadsChosed : MonoBehaviour
{
    public static RoadsChosed Instance { get; private set; }

    private int itemsDoor = 0;
    private int enemiesDoor = 0;

    private bool isGoodEnding = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CollectedItemDoor()
    {
        itemsDoor++;
    }

    public void EnemiesDefeatedDoor()
    {
        enemiesDoor++;
    }

    public void CheckFinal()
    {
        if(itemsDoor > enemiesDoor)
        {
            isGoodEnding = true;
        }else if(itemsDoor < enemiesDoor)
        {
            isGoodEnding = false;
        }
    }
}

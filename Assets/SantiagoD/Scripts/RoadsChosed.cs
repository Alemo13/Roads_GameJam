using System.Collections.Generic;
using UnityEngine;

public class RoadsChosed : MonoBehaviour
{
    public static RoadsChosed Instance { get; private set; }

    public FinalData_SO finalDataSO;

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
        itemsDoor = finalDataSO.itemsDoor;
        itemsDoor++;
        finalDataSO.itemsDoor = itemsDoor;
    }

    public void EnemiesDefeatedDoor()
    {
        enemiesDoor = finalDataSO.enemiesDoor;
        enemiesDoor++;
        finalDataSO.enemiesDoor = enemiesDoor;
    }

    public void CheckFinal()
    {
        itemsDoor = finalDataSO.itemsDoor;
        enemiesDoor = finalDataSO.enemiesDoor;

        if (itemsDoor > enemiesDoor)
        {
            isGoodEnding = true;
        }else if(itemsDoor < enemiesDoor)
        {
            isGoodEnding = false;
        }
    }
}

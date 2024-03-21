using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComienzoJuego : MonoBehaviour
{
    // Start is called before the first frame update
    private Button buttonStart;
    private GameManager gameManager;

    
    void Start()
    {
        buttonStart=GetComponent<Button>();
        gameManager=GameObject.Find("GameManger").GetComponent<GameManager>();

     
    }

    

}

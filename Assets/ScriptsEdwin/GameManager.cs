using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject panelMenu;
    GameObject canvasGameplay;
    GameObject panelMisionFallida;
    GameObject panelPausa;
    public GameObject pauseButton;
    public static GameManager Instance;
    public bool gameIsPaused;
    bool firstTimeGame;


     private void Awake() {
        if(GameManager.Instance==null)
        {
             GameManager.Instance=this;
             DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        panelMenu=GameObject.Find("Panel Game Menu");
        panelPausa = GameObject.Find("Panel Pausa");
        canvasGameplay=GameObject.Find("Canvas Gameplay");
        panelMisionFallida = GameObject.Find("Panel Mision Fallida");
        pauseButton=GameObject.Find("Boton Pausa");

        panelMisionFallida.SetActive(false);
        canvasGameplay.SetActive(false);
        panelPausa.SetActive(false);
        gameIsPaused = false;
        firstTimeGame = true;


    }


    public void MisionFallida(){
        panelMisionFallida.SetActive(true);
    }
        public void RestarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseButton.SetActive(true);
        gameIsPaused=false;

    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;

        if(gameIsPaused)
        {
            panelPausa.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            panelPausa.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void MapConditions()
    {
        if(firstTimeGame)
        {
            firstTimeGame = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

        }
        
    }
    void activepanelUI()
    {
         canvasGameplay.SetActive(false);
    }
}

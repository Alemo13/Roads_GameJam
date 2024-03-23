using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    GameObject panelMenu;
    GameObject canvasGameplay;
    GameObject panelMisionFallida;
    GameObject panelPausa;
    public GameObject pauseButton;
    public GameObject panelMapa;
    public GameObject canvaOpciones;
    [SerializeField] Button botonOpciones;
    public bool gameIsPaused;
    public bool firstTimeGame;

    private int actualScene = 1;


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
        panelMapa=GameObject.Find("Panel Mapa");
        canvaOpciones=GameObject.Find("Panel Config");
        panelMisionFallida.SetActive(false);
        canvasGameplay.SetActive(false);
        panelPausa.SetActive(false);
        gameIsPaused = false;
        firstTimeGame = true;
        canvaOpciones.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            PauseGame();
        }
    }

    public void MisionFallida(){
        panelMisionFallida.SetActive(true);
    }
        public void RestarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseButton.SetActive(true);
        gameIsPaused=false;
        canvaOpciones.SetActive(true);
        AudioManager.InstanceMusic.UnmuteMusic();

    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;

        if(gameIsPaused)
        {
            panelPausa.SetActive(true);
            Time.timeScale = 0;
            AudioManager.InstanceMusic.TogleMusic();
        }
        else
        {
            panelPausa.SetActive(false);
            Time.timeScale = 1;
            AudioManager.InstanceMusic.UnmuteMusic();
        }
    }


    public void ReturnMenu(){
        Debug.Log("ReturnMenu");

        pauseButton.SetActive(true);
        panelMapa.SetActive(true);
        //SceneManager.LoadScene(0);
        panelPausa.SetActive(false);
        gameIsPaused=false;
        firstTimeGame = true;
        AudioManager.InstanceMusic.UnmuteMusic();
        canvaOpciones.SetActive(false);
    }


    public void MapConditions()
    {
        if(firstTimeGame)
        {
            firstTimeGame = false;
            SceneManager.LoadScene(1);
        }
        
    }
    public void activateCanvaOpcyion()
    {
        canvaOpciones.SetActive(true);
    }

    public void NextScene()
    {
        actualScene++;
        SceneManager.LoadScene(actualScene);
    }
}

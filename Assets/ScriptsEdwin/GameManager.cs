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
    GameObject final1;
    GameObject final2;
    GameObject final3;
    GameObject final4;

    public GameObject pauseButton;
    public GameObject panelMapa;
    public GameObject canvaOpciones;
    [SerializeField] Button botonOpciones;
    public bool gameIsPaused;
    public bool firstTimeGame;

    private int actualScene = 1;


    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        final1 = GameObject.Find("Panel Fina1");
        final2 = GameObject.Find("Panel Fina2");
        final3 = GameObject.Find("Panel Fina3");
        final4 = GameObject.Find("Panel Fina4");


        panelMenu = GameObject.Find("Panel Game Menu");
        panelPausa = GameObject.Find("Panel Pausa");
        canvasGameplay = GameObject.Find("Canvas Gameplay");
        panelMisionFallida = GameObject.Find("Panel Mision Fallida");
        pauseButton = GameObject.Find("Boton Pausa");
        panelMapa = GameObject.Find("Panel Mapa");
        canvaOpciones = GameObject.Find("Panel Config");
        panelMisionFallida.SetActive(false);
        canvasGameplay.SetActive(false);
        panelPausa.SetActive(false);
        gameIsPaused = false;
        firstTimeGame = true;
        canvaOpciones.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PauseGame();
        }
        if (!gameIsPaused && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void MisionFallida()
    {
        panelMisionFallida.SetActive(true);
    }
    public void RestarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseButton.SetActive(true);
        gameIsPaused = false;
        if (canvaOpciones != null) { canvaOpciones.SetActive(true); }

        AudioManager.InstanceMusic.UnmuteMusic();
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
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


    public void ReturnMenu()
    {
        Debug.Log("ReturnMenu");

        pauseButton.SetActive(true);
        panelMapa.SetActive(true);
        panelPausa.SetActive(false);
        AudioManager.InstanceMusic.UnmuteMusic();
        canvaOpciones.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        gameIsPaused = false;
        AudioManager.InstanceMusic.UnmuteMusic();
        pauseButton.SetActive(true);
        panelMapa.SetActive(true);
        firstTimeGame = true;
        panelPausa.SetActive(false);
        canvaOpciones.SetActive(false);
        SceneManager.LoadScene(0);
    }


    public void MapConditions()
    {
        if (firstTimeGame)
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

    public void FinalScene(int final)
    {
        switch (final)
        {
            case 0:
                final4.SetActive(true);
                break;
            case 1:
                final3.SetActive(true);
                break;
            case 2:
                final2.SetActive(true);
                break;
            case 3:
                final1.SetActive(true);
                break;
        }

    }
}

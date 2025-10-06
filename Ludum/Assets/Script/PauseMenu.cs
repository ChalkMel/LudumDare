using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private Button exitMenuYesBut;
    [SerializeField] private Button exitMenuNoBut;
    [SerializeField] private Button exitBut;
    [SerializeField] private Button resumeBut;

    private void Awake()
    {

        exitBut.onClick.AddListener(ExitGameMenu);
        resumeBut.onClick.AddListener(Resume);
    }
    void ExitGameMenu()
    {
        exitMenu.SetActive(true);
        exitMenuYesBut.onClick.AddListener(ExitGame);
        exitMenuNoBut.onClick.AddListener(Return);
        
    }
    void ExitGame()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }
    void Return()
    {
        exitMenu.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}

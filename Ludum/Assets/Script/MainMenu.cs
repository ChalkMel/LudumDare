using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private Button exitMenuYesBut;
    [SerializeField] private Button exitMenuNoBut;
    [SerializeField] private Button exitBut;
    [SerializeField] private Button playBut;
    private void Awake()
    {
        
        exitBut.onClick.AddListener(ExitGameMenu);
        playBut.onClick.AddListener(StartGame);
    }
    void ExitGameMenu()
    {
        exitMenu.SetActive(true);
        exitMenuYesBut.onClick.AddListener(ExitGame);
        exitMenuNoBut.onClick.AddListener(Return);
    }
    void ExitGame()
    {
        Application.Quit();
    }
    void Return()
    {
        exitMenu.SetActive(false);
    }
   
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}

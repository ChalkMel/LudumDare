using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private Button exitMenuYesBut;
    [SerializeField] private Button exitMenuNoBut;
    [SerializeField] private Button exitBut;
    [SerializeField] private Button settingsBut;
    [SerializeField] private Button settingsMenuNoBut;
    [SerializeField] private Button playBut;
    private void Awake()
    {
        
        exitBut.onClick.AddListener(ExitGameMenu);
        settingsBut.onClick.AddListener(SettingsMenu);
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
        settingsMenu.SetActive(false);
    }
    void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        settingsMenuNoBut.onClick.AddListener(Return);
    }
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}

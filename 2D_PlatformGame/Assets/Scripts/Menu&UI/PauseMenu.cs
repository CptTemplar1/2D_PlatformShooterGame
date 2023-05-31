using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public String mainMenuScene; // pole okreœlaj¹ce nazwê sceny z menu g³ównym

    public static bool GameIsPaused = false; // flaga okreœlaj¹ca czy gra jest zapauzowana

    public GameObject pauseMenuUI; // schowany obiekt z interfejsem Menu, który jest pokazywany po w³¹czeniu pauseMenu

    private void Update()
    {
        // przycisk ESC w³¹cza PauseMenu
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPaused) 
                ResumeGame();
            else
                PauseGame();
        }
    }

    // metoda pauzuj¹ca grê
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true; // Zapauzuj wszystkie dŸwiêki
        GameIsPaused = true;
    }

    // metoda wznawiaj¹ca grê
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false; // Odpauzuj wszystkie dŸwiêki
        GameIsPaused = false;
    }

    // metoda wychodz¹ca z gry do Menu g³ównego
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; // Odpauzuj wszystkie dŸwiêki
        SceneManager.LoadScene(mainMenuScene);
    }

    // metoda wychodz¹ca z gry do pulpitu
    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry. Nie dzia³a w edytorze Unity");
        Application.Quit();
    }
}

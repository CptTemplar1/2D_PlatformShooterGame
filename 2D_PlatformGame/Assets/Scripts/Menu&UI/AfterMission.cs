using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterMission : MonoBehaviour
{
    public String mainMenuScene; // pole okreœlaj¹ce nazwê sceny z menu g³ównym

    public static bool GameIsPaused = false; // flaga okreœlaj¹ca czy gra jest zapauzowana

    public GameObject afterMissionUI; // schowany obiekt z interfejsem Menu, który jest pokazywany po w³¹czeniu pauseMenu


    // metoda pauzuj¹ca grê
    public void PauseGame()
    {
        afterMissionUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // metoda ³aduj¹ca nastêpny level
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex < totalSceneCount - 1) // sprawdzenie, czy istnieje nastêpna scena
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(mainMenuScene);
            Debug.Log("Nie ma wiêcej poziomów, wyjdŸ do menu!"); // obs³uga przypadku, gdy nie ma wiêcej poziomów
        }
    }

    // metoda wychodz¹ca z gry do Menu g³ównego
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}

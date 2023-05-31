using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterMission : MonoBehaviour
{
    public String mainMenuScene; // pole okreœlaj¹ce nazwê sceny z menu g³ównym

    public static bool GameIsPaused = false; // flaga okreœlaj¹ca czy gra jest zapauzowana

    public GameObject afterSuccessfulMissionUI; // schowany obiekt z interfejsem Menu, który jest pokazywany po wygranym poziomie
    public GameObject afterFailedMissionUI; // schowany obiekt z interfejsem Menu, który jest pokazywany po wprzegranym poziomie

    // metoda pauzuj¹ca grê i w³¹czaj¹ca odpowiedni panel w zale¿noœci czy runda zosta³a wygrana
    public void FinishLevel(bool success)
    {
        if (success)
        {
            afterSuccessfulMissionUI.SetActive(true);
        }
        else
        {
            afterFailedMissionUI.SetActive(true);
        }

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
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuScene);
            Debug.Log("Nie ma wiêcej poziomów, wychodzê do menu!"); // obs³uga przypadku, gdy nie ma wiêcej poziomów
        }
    }

    // metoda ³aduj¹ca od nowa ten sam level
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // metoda wychodz¹ca z gry do Menu g³ównego
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}

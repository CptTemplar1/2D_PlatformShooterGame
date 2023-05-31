using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterMission : MonoBehaviour
{
    public String mainMenuScene; // pole okre�laj�ce nazw� sceny z menu g��wnym

    public static bool GameIsPaused = false; // flaga okre�laj�ca czy gra jest zapauzowana

    public GameObject afterMissionUI; // schowany obiekt z interfejsem Menu, kt�ry jest pokazywany po w��czeniu pauseMenu


    // metoda pauzuj�ca gr�
    public void PauseGame()
    {
        afterMissionUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // metoda �aduj�ca nast�pny level
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex < totalSceneCount - 1) // sprawdzenie, czy istnieje nast�pna scena
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(mainMenuScene);
            Debug.Log("Nie ma wi�cej poziom�w, wyjd� do menu!"); // obs�uga przypadku, gdy nie ma wi�cej poziom�w
        }
    }

    // metoda wychodz�ca z gry do Menu g��wnego
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterMission : MonoBehaviour
{
    public String mainMenuScene; // pole okre�laj�ce nazw� sceny z menu g��wnym

    public static bool GameIsPaused = false; // flaga okre�laj�ca czy gra jest zapauzowana

    public GameObject afterSuccessfulMissionUI; // schowany obiekt z interfejsem Menu, kt�ry jest pokazywany po wygranym poziomie
    public GameObject afterFailedMissionUI; // schowany obiekt z interfejsem Menu, kt�ry jest pokazywany po wprzegranym poziomie

    // metoda pauzuj�ca gr� i w��czaj�ca odpowiedni panel w zale�no�ci czy runda zosta�a wygrana
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
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuScene);
            Debug.Log("Nie ma wi�cej poziom�w, wychodz� do menu!"); // obs�uga przypadku, gdy nie ma wi�cej poziom�w
        }
    }

    // metoda �aduj�ca od nowa ten sam level
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // metoda wychodz�ca z gry do Menu g��wnego
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}

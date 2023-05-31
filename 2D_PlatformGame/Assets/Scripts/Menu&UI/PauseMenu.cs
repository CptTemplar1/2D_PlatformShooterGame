using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public String mainMenuScene; // pole okre�laj�ce nazw� sceny z menu g��wnym

    public static bool GameIsPaused = false; // flaga okre�laj�ca czy gra jest zapauzowana

    public GameObject pauseMenuUI; // schowany obiekt z interfejsem Menu, kt�ry jest pokazywany po w��czeniu pauseMenu

    private void Update()
    {
        // przycisk ESC w��cza PauseMenu
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPaused) 
                ResumeGame();
            else
                PauseGame();
        }
    }

    // metoda pauzuj�ca gr�
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true; // Zapauzuj wszystkie d�wi�ki
        GameIsPaused = true;
    }

    // metoda wznawiaj�ca gr�
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false; // Odpauzuj wszystkie d�wi�ki
        GameIsPaused = false;
    }

    // metoda wychodz�ca z gry do Menu g��wnego
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; // Odpauzuj wszystkie d�wi�ki
        SceneManager.LoadScene(mainMenuScene);
    }

    // metoda wychodz�ca z gry do pulpitu
    public void QuitGame()
    {
        Debug.Log("Wyj�cie z gry. Nie dzia�a w edytorze Unity");
        Application.Quit();
    }
}

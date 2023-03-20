using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // metoda sprawdzaj�ca jaka jest aktualna scena gry i �aduj�ca nast�pn�
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // metoda zamykania gry
    public void QuitGame()
    {
        Debug.Log("Wychodzenie z gry dzia�a");
        Application.Quit();
    }
}

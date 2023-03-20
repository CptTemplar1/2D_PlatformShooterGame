using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // metoda sprawdzaj¹ca jaka jest aktualna scena gry i ³aduj¹ca nastêpn¹
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // metoda zamykania gry
    public void QuitGame()
    {
        // wyjœcie z gry nie dzia³a w Unity, wiêc sprawdzamy debugiem
        Debug.Log("Wychodzenie z gry dzia³a");
        Application.Quit();
    }
}

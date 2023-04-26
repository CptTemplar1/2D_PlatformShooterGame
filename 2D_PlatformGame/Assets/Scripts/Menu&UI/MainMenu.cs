using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName;

    // metoda sprawdzaj�ca jaka jest aktualna scena gry i �aduj�ca nast�pn�
    public void PlayGame() 
    {
        SceneManager.LoadScene(gameSceneName);
    }

    //metoda u�ywana do resetowania poiadanej broni na pocz�tku rozgrywki
    public void StartNewGame()
    {
        NewGameStatic.setNewGame();
    }

    // metoda zamykania gry
    public void QuitGame()
    {
        // wyj�cie z gry nie dzia�a w Unity, wi�c sprawdzamy debugiem
        Debug.Log("Wychodzenie z gry dzia�a");
        Application.Quit();
    }
}

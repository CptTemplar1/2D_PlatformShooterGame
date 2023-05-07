using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //metoda u¿ywana do resetowania poiadanej broni na pocz¹tku rozgrywki
    public void StartNewGame()
    {
        NewGameStatic.setNewGame();
    }

    // metoda zamykania gry
    public void QuitGame()
    {
        // wyjœcie z gry nie dzia³a w Unity, wiêc sprawdzamy debugiem
        Debug.Log("Wychodzenie z gry dzia³a");
        Application.Quit();
    }

    //metoda do weapon shop wypisuj¹ca ceny
    public void setWeaponShop()
    {
        ShopController.FindAnyObjectByType<ShopController>().setPrices();
    }

    //metoda do armor shop wypisuj¹ca ceny
    public void setArmorShop()
    {
        ShopArmorController.FindAnyObjectByType<ShopArmorController>().setPrices();
    }
}

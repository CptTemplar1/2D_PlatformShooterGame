using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> passedImages = new List<GameObject>(); // lista przechowuj¹ca wygrane levle

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

    //metoda pokazuj¹ca na mapie które lvl uda³o siê przejœæ
    public void showCompletedLevels()
    {
        if (PassedLevels.passedLevels[1] == true)
            passedImages[0].gameObject.SetActive(true);
        if (PassedLevels.passedLevels[2] == true)
            passedImages[1].gameObject.SetActive(true);
        if (PassedLevels.passedLevels[3] == true)
            passedImages[2].gameObject.SetActive(true);
        if (PassedLevels.passedLevels[4] == true)
            passedImages[3].gameObject.SetActive(true);
        if (PassedLevels.passedLevels[5] == true)
            passedImages[4].gameObject.SetActive(true);
        if (PassedLevels.passedLevels[6] == true)
            passedImages[5].gameObject.SetActive(true);
        if (PassedLevels.passedLevels[7] == true)
            passedImages[6].gameObject.SetActive(true);
        if (PassedLevels.passedLevels[8] == true)
            passedImages[7].gameObject.SetActive(true);
        if (PassedLevels.passedLevels[9] == true)
            passedImages[8].gameObject.SetActive(true);
    }
}

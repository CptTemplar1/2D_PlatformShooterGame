using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> passedImages = new List<GameObject>(); // lista przechowuj�ca wygrane levle

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

    //metoda do weapon shop wypisuj�ca ceny
    public void setWeaponShop()
    {
        ShopController.FindAnyObjectByType<ShopController>().setPrices();
    }

    //metoda do armor shop wypisuj�ca ceny
    public void setArmorShop()
    {
        ShopArmorController.FindAnyObjectByType<ShopArmorController>().setPrices();
    }

    //metoda pokazuj�ca na mapie kt�re lvl uda�o si� przej��
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

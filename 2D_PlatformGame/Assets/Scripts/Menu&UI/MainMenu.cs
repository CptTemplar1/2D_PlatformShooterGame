using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> passedImages = new List<GameObject>(); // lista przechowuj�ca wygrane levle

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


    private void Update()
    {
        //Resetowanie gry przy pomocy klawiszy CTRL + ALT + R
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        PlayerPrefs.DeleteAll(); //usuni�cie wszystkich zapisanych rzeczy (czy�ci playerPrefs)
        //PlayerPrefs.SetInt("isStarted", 0); //ustawienie playerPrefs okre�laj�cego czy to pierwsza gra na 0 (false)

        Application.Quit(); //wyj�cie z gry po resecie
        Debug.Log("Zresetowano PlayerPrefs");
    }
}

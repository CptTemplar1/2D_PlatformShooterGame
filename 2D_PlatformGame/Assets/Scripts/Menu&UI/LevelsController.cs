using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsController : MonoBehaviour
{
    // metoda wczytuj�ca pierwszy poziom
    public void PlayEarth1()
    {
        SceneManager.LoadScene(1);
    }
    // metoda wczytuj�ca drugi poziom
    public void PlayJupiter2()
    {
        SceneManager.LoadScene(2);
    }
    // metoda wczytuj�ca trzeci poziom
    public void PlayMars3()
    {
        SceneManager.LoadScene(3);
    }
}

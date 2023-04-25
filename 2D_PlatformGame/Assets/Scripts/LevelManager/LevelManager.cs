using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int numberOfEnemies;
    private AfterMission afterMission; //komponent AfterMission z okna zakoÒczenia levelu

    private void Start()
    {
        // Znajdü wszystkie obiekty w scenie z tagiem "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numberOfEnemies = enemies.Length;

        afterMission = GameObject.Find("AfterMission").GetComponent<AfterMission>();
    }

    public void EnemyKilled()
    {
        numberOfEnemies--;

        // Sprawdü, czy wszyscy przeciwnicy zostali zabici
        if (numberOfEnemies <= 0)
        {
            // Jeúli tak, to otwÛrz panel zakoÒczenia levelu
            StartCoroutine(pauseAfterLevel());
        }
    }
    //metoda wykonywana po zadanym czasie
    System.Collections.IEnumerator pauseAfterLevel()
    {
        yield return new WaitForSeconds(5);
        afterMission.PauseGame();
    }
}

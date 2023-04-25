using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int numberOfEnemies;
    private AfterMission afterMission; //komponent AfterMission z okna zakoñczenia levelu
    private void Start()
    {
        StartCoroutine(findEnemies());

        afterMission = GameObject.Find("AfterMission").GetComponent<AfterMission>();
    }

    public void EnemyKilled()
    {
        numberOfEnemies--;

        // SprawdŸ, czy wszyscy przeciwnicy zostali zabici
        if (numberOfEnemies <= 0)
        {
            // Jeœli tak, to otwórz panel zakoñczenia levelu
            StartCoroutine(pauseAfterLevel());
        }
    }
    //metoda wykonywana po zadanym czasie
    System.Collections.IEnumerator pauseAfterLevel()
    {
        yield return new WaitForSeconds(5);
        afterMission.PauseGame();
    }

    //metoda znajduj¹ca wszystkie obiekty "Enemy"
    System.Collections.IEnumerator findEnemies()
    {
        yield return new WaitForSeconds(1);
        // ZnajdŸ wszystkie obiekty w scenie z tagiem "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numberOfEnemies = enemies.Length;
    }
}

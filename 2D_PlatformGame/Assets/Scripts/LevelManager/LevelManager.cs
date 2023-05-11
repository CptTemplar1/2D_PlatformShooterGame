using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int numberOfEnemies;
    private AfterMission afterMission; //komponent AfterMission z okna zako�czenia levelu
    private CoinsHandler coinsHandler; //komponent coinHandler zarzadzajacy coinami
    private EnemiesCounter enemiesCounter; //komponent enemiesCounter zarzadzajacy iloscia przeciwnikow

    private void Start()
    {
        StartCoroutine(findEnemies());

        afterMission = GameObject.Find("AfterMission").GetComponent<AfterMission>();
        coinsHandler = GameObject.Find("CollectedMoney").GetComponent<CoinsHandler>();
        enemiesCounter = GameObject.Find("EnemiesCounter").GetComponent<EnemiesCounter>();
    }

    public void EnemyKilled()
    {
        numberOfEnemies--;

        // Sprawd�, czy wszyscy przeciwnicy zostali zabici
        if (numberOfEnemies <= 0)
        {
            // Je�li tak, to otw�rz panel zako�czenia levelu
            StartCoroutine(pauseAfterLevel());
        }
    }
    //metoda wykonywana po zadanym czasie
    System.Collections.IEnumerator pauseAfterLevel()
    {
        yield return new WaitForSeconds(5);
        //dodaj coiny do wszystkich coinow gracza
        StaticCoins.add(coinsHandler.coins);
        PassedLevels.setPassedLevel(SceneManager.GetActiveScene().buildIndex);
        afterMission.PauseGame();
    }

    //metoda znajduj�ca wszystkie obiekty "Enemy"
    System.Collections.IEnumerator findEnemies()
    {
        yield return new WaitForSeconds(0.1f);
        // Znajd� wszystkie obiekty w scenie z tagiem "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Count() == 0 )
        {
            enemies = GameObject.FindGameObjectsWithTag("Boss");
        }
        numberOfEnemies = enemies.Length;
        enemiesCounter.setEnemiesCounter(numberOfEnemies);
    }
}

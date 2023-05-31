using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int numberOfEnemies;
    private AfterMission afterMission; //komponent AfterMission z okna zakoñczenia levelu
    private CoinsHandler coinsHandler; //komponent coinHandler zarzadzajacy coinami
    private EnemiesCounter enemiesCounter; //komponent enemiesCounter zarzadzajacy iloscia przeciwnikow
    private PauseMenu pauseMenu;

    float time; //czas do odliczania po np zakonczeniu lvla
    bool startPassedLevelCounter = false;
    bool startDeathCounter = false;
    private GameObject timeCounter;
    private TMP_Text timeCounterText;

    public GameObject playerConfetti; //obiekt przechowuj¹cy konfetti nad graczem. Konfetti uruchamiamy po wygranym levelu

    private void Start()
    {
        StartCoroutine(findEnemies());
        time = 5;
        afterMission = GameObject.Find("AfterMission").GetComponent<AfterMission>();
        coinsHandler = GameObject.Find("CollectedMoney").GetComponent<CoinsHandler>();
        enemiesCounter = GameObject.Find("EnemiesCounter").GetComponent<EnemiesCounter>();
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();

        timeCounter = GameObject.Find("TimeCounter");
        timeCounterText = GameObject.Find("Counter").GetComponent<TMP_Text>();
        timeCounter.SetActive(false);
    }

    private void Update()
    {
        if(startPassedLevelCounter == true)
        {
            playerConfetti.SetActive(true);//w³¹czenie konfetti nad graczem po wygranej

            timeCounter.SetActive(true);
            time -= Time.deltaTime;
            timeCounterText.text = Mathf.Clamp(Mathf.CeilToInt(time), 0, int.MaxValue).ToString();

            if (time <= 0)
            {
                timeCounter.SetActive(false);
                pauseAfterLevel();
                startPassedLevelCounter = false;
            }
        }
        if(startDeathCounter == true)
        {
            timeCounter.SetActive(true);
            time -= Time.deltaTime;
            timeCounterText.text = Mathf.Clamp(Mathf.CeilToInt(time), 0, int.MaxValue).ToString();

            if (time <= 0)
            {
                timeCounter.SetActive(false);
                pauseMenu.PauseGame();
                startDeathCounter = false;
            }
        }
    }

    public void playerIsDead()
    {
        startDeathCounter = true;
    }

    public void EnemyKilled()
    {
        numberOfEnemies--;

        // SprawdŸ, czy wszyscy przeciwnicy zostali zabici
        if (numberOfEnemies <= 0)
        {
            // Jeœli tak, to otwórz panel zakoñczenia levelu
            startPassedLevelCounter = true;
        }
    }
    //metoda wykonywana po zadanym czasie
    void pauseAfterLevel()
    {
        //dodaj coiny do wszystkich coinow gracza
        StaticCoins.add(coinsHandler.coins);
        PassedLevels.setPassedLevel(SceneManager.GetActiveScene().buildIndex);
        afterMission.PauseGame();
    }

    //metoda znajduj¹ca wszystkie obiekty "Enemy"
    System.Collections.IEnumerator findEnemies()
    {
        yield return new WaitForSeconds(0.1f);
        // ZnajdŸ wszystkie obiekty w scenie z tagiem "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Count() == 0 )
        {
            enemies = GameObject.FindGameObjectsWithTag("Boss");
        }
        numberOfEnemies = enemies.Length;
        enemiesCounter.setEnemiesCounter(numberOfEnemies);
    }
}

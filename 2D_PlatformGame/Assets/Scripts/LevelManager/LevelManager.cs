using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int numberOfEnemies;
    private AfterMission afterMission; //komponent AfterMission z okna zako�czenia levelu
    private CoinsHandler coinsHandler; //komponent coinHandler zarzadzajacy coinami
    private EnemiesCounter enemiesCounter; //komponent enemiesCounter zarzadzajacy iloscia przeciwnikow
    private PauseMenu pauseMenu;

    float time; //czas do odliczania po np zakonczeniu lvla
    bool startPassedLevelCounter = false;
    bool startDeathCounter = false;
    private GameObject timeCounter;
    private TMP_Text timeCounterText;

    public GameObject playerConfetti; //obiekt przechowuj�cy konfetti nad graczem. Konfetti uruchamiamy po wygranym levelu

    private AudioSource levelMusic; //�r�d�o d�wi�ku muzyki w tle dla danego levelu

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

        levelMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>(); //pobranie komponentu audio source z g��wnej kamery graj�cej muzyk� w tle
    }

    private void Update()
    {
        //odliczanie po wygranym poziomie
        if(startPassedLevelCounter == true)
        {
            playerConfetti.SetActive(true);//w��czenie konfetti nad graczem po wygranej
            EnableBackgroundMusic(false); //zastopowanie muzyki w tle

            timeCounter.SetActive(true);
            time -= Time.deltaTime;
            timeCounterText.text = Mathf.Clamp(Mathf.CeilToInt(time), 0, int.MaxValue).ToString();

            if (time <= 0)
            {
                timeCounter.SetActive(false);
                pauseAfterPassedLevel();
                startPassedLevelCounter = false;
            }
        }
        //odliczanie po przegranym poziomie
        if(startDeathCounter == true)
        {
            EnableBackgroundMusic(false); //zastopowanie muzyki w tle

            timeCounter.SetActive(true);
            time -= Time.deltaTime;
            timeCounterText.text = Mathf.Clamp(Mathf.CeilToInt(time), 0, int.MaxValue).ToString();

            if (time <= 0)
            {
                timeCounter.SetActive(false);
                pauseAfterFailedLevel();
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

        // Sprawd�, czy wszyscy przeciwnicy zostali zabici
        if (numberOfEnemies <= 0)
        {
            // Je�li tak, to otw�rz panel zako�czenia levelu
            startPassedLevelCounter = true;
        }
    }
    //metoda wykonywana po zadanym czasie
    //metoda w��czaj�ca ekran po wygranej rundzie
    void pauseAfterPassedLevel()
    {
        //dodaj coiny do wszystkich coinow gracza
        StaticCoins.add(coinsHandler.coins);
        PassedLevels.setPassedLevel(SceneManager.GetActiveScene().buildIndex);
        afterMission.FinishLevel(true);
    }

    //metoda wykonywana po zadanym czasie
    //metoda w��czaj�ca ekran po przegranej rundzie
    void pauseAfterFailedLevel()
    {
        afterMission.FinishLevel(false);
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

    //metoda zatrzymuj�ca lub wznawiaj�ca muzyk� w tle
    private void EnableBackgroundMusic(bool status)
    {
        if (status)
            levelMusic.Play();
        else
            levelMusic.Stop();
    }
}

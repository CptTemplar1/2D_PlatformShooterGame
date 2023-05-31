using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healthMax = 100; //defaultowe maksymalne zdrowie to 100
    private int health; // aktualny stan zdrowia 
    public GameObject deathEffect;
    public float detectionRange; // odleg³oœæ, w której potwór zacznie œcigaæ gracza
    protected Transform player; //do obliczania odleg³oœci od gracza
    public Animator animator;
    public float walkingDistance = 2;
    public Transform spawnPoint = null;
    public GameObject money = null;
    private bool isDead = false;

    public int collisionDamage = 25; //zmienna okreœlaj¹ca iloœæ obra¿eñ zadawanych graczowi podczas kolizji

    private float cooldownTime = 1.0f; // d³ugoœæ cooldownu przed nastêpnym otrzymaniem obra¿eñ
    private float lastHitTime = -Mathf.Infinity; // czas ostatniego otrzymania obra¿eñ

    public event EventHandler OnHealthChanged; //event podczas zmiany stanu zdrowia

    private EnemiesCounter enemiesCounter;

    private AudioSource enemyAudio; //Ÿród³o dŸwiêków przeciwnika
    public AudioClip deathSound; //dŸwiêk œmierci przeciwnika

    //zainicjowanie aktualnego ¿ycia wartoœci¹ maksymaln¹
    protected virtual void Awake()
    {
        health = healthMax;
        isDead = false;

        enemyAudio = gameObject.GetComponent<AudioSource>(); //pobranie komponentu Ÿród³a dŸwiêku
    }

    //zwraca wartoœæ aktualnego zdrowia w skali od 0 do 1
    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;

        if (OnHealthChanged != null)
            OnHealthChanged(this, EventArgs.Empty);

        if (health <= 0)
            Die();
    }

    //metoda obs³ugi leczenia przeciwnika
    protected virtual void Heal(int amount)
    {
        health += amount;
        if (health > healthMax)
            health = healthMax;

        if (OnHealthChanged != null)
            OnHealthChanged(this, EventArgs.Empty);
    }

    protected virtual void Die()
    {
        if(isDead == false)
        {
            enemyAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>(); //przypisanie Ÿród³a dŸwiêku do gracza przy œmierci potwora, bo inaczej dŸwiêk nie zd¹¿a³ siê odtworzyæ przed œmierci¹ potwora
            F3DAudio.PlayOneShotRandom(enemyAudio, deathSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dŸwiêku œmierci potwora

            enemiesCounter = GameObject.Find("EnemiesCounter").GetComponent<EnemiesCounter>();
            enemiesCounter.decreaseEnemiesCounter();
            isDead = true;
            GameObject newMoney = Instantiate(money);
            newMoney.transform.position = transform.position;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            // Wywo³aj metodê EnemyKilled() w skrypcie LevelManager
            GameObject levelManagerObject = GameObject.FindWithTag("LevelManager");
            LevelManager levelManager = levelManagerObject.GetComponent<LevelManager>();
            levelManager.EnemyKilled();
        }
    }

    //kolizja z playerem i zadanie mu obra¿eñ
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Sprawdzenie, czy up³yn¹³ wystarczaj¹co d³ugi czas od ostatniego uderzenia
            if (Time.time - lastHitTime > cooldownTime)
            {
                F3DCharacter character = collision.gameObject.GetComponent<F3DCharacter>();
                if (character != null)
                {
                    character.OnDamage(collisionDamage, false);
                }

                // Zapisanie czasu ostatniego uderzenia
                lastHitTime = Time.time;
            }
        }
    }

}

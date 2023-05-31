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
    public float detectionRange; // odleg�o��, w kt�rej potw�r zacznie �ciga� gracza
    protected Transform player; //do obliczania odleg�o�ci od gracza
    public Animator animator;
    public float walkingDistance = 2;
    public Transform spawnPoint = null;
    public GameObject money = null;
    private bool isDead = false;

    public int collisionDamage = 25; //zmienna okre�laj�ca ilo�� obra�e� zadawanych graczowi podczas kolizji

    private float cooldownTime = 1.0f; // d�ugo�� cooldownu przed nast�pnym otrzymaniem obra�e�
    private float lastHitTime = -Mathf.Infinity; // czas ostatniego otrzymania obra�e�

    public event EventHandler OnHealthChanged; //event podczas zmiany stanu zdrowia

    private EnemiesCounter enemiesCounter;

    private AudioSource enemyAudio; //�r�d�o d�wi�k�w przeciwnika
    public AudioClip deathSound; //d�wi�k �mierci przeciwnika

    //zainicjowanie aktualnego �ycia warto�ci� maksymaln�
    protected virtual void Awake()
    {
        health = healthMax;
        isDead = false;

        enemyAudio = gameObject.GetComponent<AudioSource>(); //pobranie komponentu �r�d�a d�wi�ku
    }

    //zwraca warto�� aktualnego zdrowia w skali od 0 do 1
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

    //metoda obs�ugi leczenia przeciwnika
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
            enemyAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>(); //przypisanie �r�d�a d�wi�ku do gracza przy �mierci potwora, bo inaczej d�wi�k nie zd��a� si� odtworzy� przed �mierci� potwora
            F3DAudio.PlayOneShotRandom(enemyAudio, deathSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie d�wi�ku �mierci potwora

            enemiesCounter = GameObject.Find("EnemiesCounter").GetComponent<EnemiesCounter>();
            enemiesCounter.decreaseEnemiesCounter();
            isDead = true;
            GameObject newMoney = Instantiate(money);
            newMoney.transform.position = transform.position;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            // Wywo�aj metod� EnemyKilled() w skrypcie LevelManager
            GameObject levelManagerObject = GameObject.FindWithTag("LevelManager");
            LevelManager levelManager = levelManagerObject.GetComponent<LevelManager>();
            levelManager.EnemyKilled();
        }
    }

    //kolizja z playerem i zadanie mu obra�e�
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Sprawdzenie, czy up�yn�� wystarczaj�co d�ugi czas od ostatniego uderzenia
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

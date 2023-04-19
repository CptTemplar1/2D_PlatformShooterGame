using System;
using System.Collections;
using System.Collections.Generic;
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

    public event EventHandler OnHealthChanged; //event podczas zmiany stanu zdrowia

    //zainicjowanie aktualnego �ycia warto�ci� maksymaln�
    private void Awake()
    {
        health = healthMax;
    }

    //zwraca warto�� aktualnego zdrowia w skali od 0 do 1
    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void TakeDamage(int damage)
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
    public void Heal(int amount)
    {
        health += amount;
        if (health > healthMax)
            health = healthMax;

        if (OnHealthChanged != null)
            OnHealthChanged(this, EventArgs.Empty);
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        // Wywo�aj metod� EnemyKilled() w skrypcie LevelManager
        GameObject levelManagerObject = GameObject.FindWithTag("LevelManager");
        LevelManager levelManager = levelManagerObject.GetComponent<LevelManager>();
        levelManager.EnemyKilled();
    }
}

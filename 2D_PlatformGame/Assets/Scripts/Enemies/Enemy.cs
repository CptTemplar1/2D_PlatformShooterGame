using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; //defaultowe zdrowie to 100
    public GameObject deathEffect;
    public float detectionRange; // odleg�o��, w kt�rej potw�r zacznie �ciga� gracza
    protected Transform player; //do obliczania odleg�o�ci od gracza
    public Animator animator;  

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

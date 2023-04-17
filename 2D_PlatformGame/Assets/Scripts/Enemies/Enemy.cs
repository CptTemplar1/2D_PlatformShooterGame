using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; //defaultowe zdrowie to 100
    public GameObject deathEffect;
    public float detectionRange; // odleg³oœæ, w której potwór zacznie œcigaæ gracza
    protected Transform player; //do obliczania odleg³oœci od gracza
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

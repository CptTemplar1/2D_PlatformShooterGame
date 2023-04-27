using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour 
{
    public int collisionDamage = 10; //zmienna określająca ilość obrażeń zadawanych graczowi podczas kolizji

    public float cooldownTime = 1.5f; // długość cooldownu przed następnym otrzymaniem obrażeń
    private float lastHitTime = -Mathf.Infinity; // czas ostatniego otrzymania obrażeń

    protected HealthStatus healthStatus; // referencja do komponentu życia

    [HideInInspector]
    public bool isDead;

    protected virtual void Awake()
    {
        healthStatus = GetComponent<HealthStatus>();
    }

    protected virtual void Update()
    {
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && isDead == false)
        {
            // Sprawdzenie, czy upłynął wystarczająco długi czas od ostatniego uderzenia
            if (Time.time - lastHitTime > cooldownTime)
            {
                F3DCharacter character = collision.gameObject.GetComponent<F3DCharacter>();
                if (character != null)
                {
                    character.OnDamage(collisionDamage);
                }

                // Zapisanie czasu ostatniego uderzenia
                lastHitTime = Time.time;
            }
        }
    }

    //metoda obsługująca umieranie przeciwnika
    public virtual void TakeDamage(int damage)
    {
        healthStatus.health -= damage;
        if (healthStatus.health < 0)
            healthStatus.health = 0;

        if (healthStatus.health <= 0)
            Die();
    }

    //metoda obsługi leczenia przeciwnika
    protected void Heal(int amount)
    {
        healthStatus.health += amount;
        if (healthStatus.health > healthStatus.maxHealth)
            healthStatus.health = healthStatus.maxHealth;
    }

    //metoda obsługi umierania przeciwnika
    protected virtual void Die()
    {
        if (isDead == false)
        {
            isDead = true;
        }
    }
}

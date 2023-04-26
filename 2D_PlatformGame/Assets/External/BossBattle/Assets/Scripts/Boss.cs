using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour 
{
    //public int damage;
    //private float timeBtwDamage = 1.5f;

    public int collisionDamage = 25; //zmienna określająca ilość obrażeń zadawanych graczowi podczas kolizji

    private float cooldownTime = 1.0f; // długość cooldownu przed następnym otrzymaniem obrażeń
    private float lastHitTime = -Mathf.Infinity; // czas ostatniego otrzymania obrażeń

    private HealthStatus healthStatus; // referencja do komponentu życia

    //public Animator camAnim;
    public Slider healthBar;
    private Animator anim;

    [HideInInspector]
    public bool isDead;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        healthStatus = GetComponent<HealthStatus>();
    }

    private void Update()
    {
        if (healthStatus.health <= healthStatus.maxHealth / 3) {
            anim.SetTrigger("stageTwo");
        }

        if (healthStatus.health <= 0) {
            anim.SetTrigger("death");
        }

        //// give the player some time to recover before taking more damage !
        //if (timeBtwDamage > 0) {
        //    timeBtwDamage -= Time.deltaTime;
        //}

        //healthBar.value = health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player") && isDead == false) {
        //    if (timeBtwDamage <= 0) {
        //        F3DCharacter character = collision.gameObject.GetComponent<F3DCharacter>();
        //        if (character != null)
        //        {
        //            character.OnDamage(damage);
        //        }
        //    }
        //}

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
            }
        }
    }

    public void TakeDamage(int damage)
    {
        healthStatus.health -= damage;
        if (healthStatus.health < 0)
            healthStatus.health = 0;
    }
}

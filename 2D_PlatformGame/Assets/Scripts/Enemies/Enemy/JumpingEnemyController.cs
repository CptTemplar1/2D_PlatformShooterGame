using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpingEnemyController : Enemy
{
    public float speed; // pr�dko��, z jak� potw�r b�dzie si� porusza�
    public float jumpForce; // si�a skoku
    public float jumpCooldown; // czas cooldownu mi�dzy skokami
    private bool isJumping = false; //flaga dla pojedynczego skoku
    private float jumpTimer = 0f; //licznik czasu mi�dzy skokami

    public UnityEvent OnLandEvent;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        jumpTimer -= Time.deltaTime; //odejmowanie czasu od licznika skoku

        //je�li gracz jest w zasi�gu wykrywania i up�yn�� czas cooldownu skoku
        if (Vector2.Distance(transform.position, player.position) < detectionRange && !isJumping && jumpTimer <= 0f)
        {
            //zmiana parametru animatora na skakanie
            animator.SetBool("isJumping", true);
            isJumping = true;

            // okre�lenie kierunku ruchu potwora
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            //skakanie potwora w kierunku gracza
            rb.velocity = new Vector2(direction.x * speed, jumpForce);

            jumpTimer = jumpCooldown; //ustawienie czasu cooldownu skoku
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
        }
    }
}

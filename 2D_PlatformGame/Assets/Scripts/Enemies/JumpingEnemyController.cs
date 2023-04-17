using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpingEnemyController : Enemy
{
    public float speed; // prêdkoœæ, z jak¹ potwór bêdzie siê porusza³
    public float jumpForce; // si³a skoku
    private bool isJumping = false; //flaga dla pojedynczego skoku

    public UnityEvent OnLandEvent;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //jeœli gracz jest w zasiêgu wykrywania
        if (Vector2.Distance(transform.position, player.position) < detectionRange && !isJumping)
        {
            //zmiana parametru animatora na skakanie
            animator.SetBool("isJumping", true);
            isJumping = true;

            // okreœlenie kierunku ruchu potwora
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            //skakanie potwora w kierunku gracza
            rb.velocity = new Vector2(direction.x * speed, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
        }
    }
}
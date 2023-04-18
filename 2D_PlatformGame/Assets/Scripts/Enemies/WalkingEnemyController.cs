using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyController : Enemy
{
    public float speed; // prêdkoœæ, z jak¹ potwór bêdzie siê porusza³

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
    }

    void Update()
    {
        //jeœli gracz jest w zasiêgu wykrywania
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            //zmiana parametru animatora na chodzenie
            animator.SetBool("isWalking", true);

            // okreœlenie kierunku ruchu potwora
            Vector2 direction = player.position - transform.position;
            direction = new Vector2(Mathf.Sign(direction.x), 0f);

            // obracanie potwora w lewo lub prawo w zale¿noœci od kierunku, w którym znajduje siê gracz
            if (direction.x > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            // poruszanie potwora w kierunku gracza tylko w prawo lub w lewo
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}

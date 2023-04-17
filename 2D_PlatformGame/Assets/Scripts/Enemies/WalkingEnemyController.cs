using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyController : Enemy
{
    public float speed; // pr�dko��, z jak� potw�r b�dzie si� porusza�

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
    }

    void Update()
    {
        //je�li gracz jest w zasi�gu wykrywania
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            //zmiana parametru animatora na chodzenie
            animator.SetBool("isWalking", true);

            // okre�lenie kierunku ruchu potwora
            Vector2 direction = player.position - transform.position;
            direction = new Vector2(Mathf.Sign(direction.x), 0f);

            // poruszanie potwora w kierunku gracza tylko w prawo lub w lewo
            if (direction.x > 0 && transform.position.x < player.position.x)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else if (direction.x < 0 && transform.position.x > player.position.x)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        else
            animator.SetBool("isWalking", false);
    }
}

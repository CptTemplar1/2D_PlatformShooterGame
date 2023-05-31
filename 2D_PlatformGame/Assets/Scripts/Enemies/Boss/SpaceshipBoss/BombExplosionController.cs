using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionController : MonoBehaviour
{

    private float cooldownTime = 2.0f; // d³ugoœæ cooldownu przed nastêpnym otrzymaniem obra¿eñ
    private float lastHitTime = -Mathf.Infinity; // czas ostatniego otrzymania obra¿eñ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Sprawdzenie, czy up³yn¹³ wystarczaj¹co d³ugi czas od ostatniego uderzenia
            if (Time.time - lastHitTime > cooldownTime)
            {
                F3DCharacter playerCharacter = collision.GetComponentInParent<F3DCharacter>();
                playerCharacter.OnDamage(50, true); //zadanie graczowi 50 obra¿eñ od bomby

                Rigidbody2D playerRigidbody = collision.GetComponentInParent<Rigidbody2D>(); // Pobranie RB gracza
                if (playerRigidbody != null)
                {
                    // Okreœlenie po³o¿enia wybuchu i gracza
                    Vector2 explosionPosition = transform.position;
                    Vector2 playerPosition = playerRigidbody.transform.position;

                    // Okreœlenie wektora odleg³oœci miêdzy wybuchem a graczem
                    Vector2 distanceVector = playerPosition - explosionPosition;

                    // Sprawdzenie po której stronie wybuchu znajduje siê gracz
                    if (distanceVector.x < 0)
                    {
                        // Odrzucenie gracza w prawo i do góry
                        Vector2 knockbackDirection = new Vector2(1f, 1f);
                        playerRigidbody.AddForce(knockbackDirection.normalized * 1999999f);
                    }
                    else if (distanceVector.x > 0)
                    {
                        // Odrzucenie gracza w lewo i do góry
                        Vector2 knockbackDirection = new Vector2(-1f, 1f);
                        playerRigidbody.AddForce(knockbackDirection.normalized * 1999999f);
                    }
                }
            }
            // Zapisanie czasu ostatniego uderzenia
            lastHitTime = Time.time;
        }
    }
}

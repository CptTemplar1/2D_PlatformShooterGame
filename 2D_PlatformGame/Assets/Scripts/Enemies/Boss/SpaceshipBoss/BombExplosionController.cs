using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionController : MonoBehaviour
{

    private float cooldownTime = 2.0f; // d�ugo�� cooldownu przed nast�pnym otrzymaniem obra�e�
    private float lastHitTime = -Mathf.Infinity; // czas ostatniego otrzymania obra�e�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Sprawdzenie, czy up�yn�� wystarczaj�co d�ugi czas od ostatniego uderzenia
            if (Time.time - lastHitTime > cooldownTime)
            {
                F3DCharacter playerCharacter = collision.GetComponentInParent<F3DCharacter>();
                playerCharacter.OnDamage(50, true); //zadanie graczowi 50 obra�e� od bomby

                Rigidbody2D playerRigidbody = collision.GetComponentInParent<Rigidbody2D>(); // Pobranie RB gracza
                if (playerRigidbody != null)
                {
                    // Okre�lenie po�o�enia wybuchu i gracza
                    Vector2 explosionPosition = transform.position;
                    Vector2 playerPosition = playerRigidbody.transform.position;

                    // Okre�lenie wektora odleg�o�ci mi�dzy wybuchem a graczem
                    Vector2 distanceVector = playerPosition - explosionPosition;

                    // Sprawdzenie po kt�rej stronie wybuchu znajduje si� gracz
                    if (distanceVector.x < 0)
                    {
                        // Odrzucenie gracza w prawo i do g�ry
                        Vector2 knockbackDirection = new Vector2(1f, 1f);
                        playerRigidbody.AddForce(knockbackDirection.normalized * 1999999f);
                    }
                    else if (distanceVector.x > 0)
                    {
                        // Odrzucenie gracza w lewo i do g�ry
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

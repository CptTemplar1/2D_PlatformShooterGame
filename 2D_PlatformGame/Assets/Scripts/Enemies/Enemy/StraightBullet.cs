using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StraightBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Zmienna przechowuj¹ca pozycjê gracza
    private Transform playerTransform;
    private Vector2 targetPosition;
    private Vector2 direction;

    public void Awake()
    {
        tag = "Bullet";
        // Pobieranie pozycji gracza
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
        // Zamiana na vector2
        targetPosition = new Vector2(playerTransform.transform.position.x, playerTransform.transform.position.y + 1.0f);

        // Pobranie referencji do Rigidbody2D pocisku
        rb = GetComponent<Rigidbody2D>();

        //Obliczenie wektora kierunku pomiêdzy aktualn¹ pozycj¹ pocisku a targetPosition
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //W tym miejscu prawdopodobnie trzeba zmieniæ kierunek lotu pocisku
        rb.velocity = direction * speed;
    }

    // metoda obs³uguj¹ca zniszczenie pocisku po zderzeniu z jak¹œ powierzchni¹
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            F3DCharacter player = collision.GetComponent<F3DCharacter>();
            if (player != null)
            {
                player.OnDamage(damage, true);
            }
            // spawnowanie efektu kolizji pocisku
            Instantiate(impactEffect, transform.position, transform.rotation);
            // usuwanie obiektu pocisku
            Destroy(gameObject);
        }
    }
}
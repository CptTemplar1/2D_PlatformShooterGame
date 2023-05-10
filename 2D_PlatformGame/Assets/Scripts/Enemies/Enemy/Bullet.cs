////###################################################
////kod, kt�ry sprawia, �e pociski pod��aj� ca�y czas za kursorem
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Bullet : MonoBehaviour
//{
//    public float speed = 20f;
//    public int damage = 40;
//    public Rigidbody2D rb;
//    public GameObject impactEffect;

//    // Update is called once per frame
//    void Update()
//    {
//        // Obliczenie kierunku ruchu pocisku wzgl�dem kursora myszy
//        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        Vector2 direction = (Vector2)((mousePosition - transform.position)).normalized;

//        // Ustawienie pr�dko�ci i kierunku ruchu pocisku
//        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
//    }

//    // metoda obs�uguj�ca zniszczenie pocisku po zderzeniu z jak�� powierzchni�
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        Enemy enemy = collision.GetComponent<Enemy>();
//        if (enemy != null)
//        {
//            enemy.TakeDamage(damage);
//        }
//        // spawnowanie efektu kolizji pocisku
//        Instantiate(impactEffect, transform.position, transform.rotation);
//        // usuwanie obiektu pocisku
//        Destroy(gameObject);
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Zmienna przechowuj�ca pozycj� gracza
    private Transform playerTransform;
    private Vector2 targetPosition;
    private Vector2 direction;

    public void Awake()
    {
        this.tag = "Bullet";
        // Pobieranie pozycji gracza
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
        // Zamiana na vector2
        targetPosition = new Vector2(playerTransform.transform.position.x, playerTransform.transform.position.y + 1.0f);

        // Pobranie referencji do Rigidbody2D pocisku
        rb = GetComponent<Rigidbody2D>();

        //Obliczenie wektora kierunku pomi�dzy aktualn� pozycj� pocisku a targetPosition
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //W tym miejscu prawdopodobnie trzeba zmieni� kierunek lotu pocisku
        rb.velocity = direction * speed;
    }

    // metoda obs�uguj�ca zniszczenie pocisku po zderzeniu z jak�� powierzchni�
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
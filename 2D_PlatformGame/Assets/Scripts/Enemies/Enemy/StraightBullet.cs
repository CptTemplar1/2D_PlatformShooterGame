using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StraightBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    private Transform target; //pozycja obiektu, w kt�rego stron� b�dzie lecia� pocisk

    private Vector2 targetPosition;
    private Vector2 direction;

    public void Awake()
    {
        tag = "Bullet";

        target = GameObject.FindGameObjectWithTag("BulletTarget").transform; // znalezienie gcelu pocisku przez tag

        float randomOffset = Random.Range(-1f, 1f); //losowanie warto�ci odchy�u dla pocisku

        // Zamiana na vector2
        targetPosition = new Vector2(target.position.x, target.position.y + randomOffset);

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
            F3DCharacter player = collision.GetComponentInParent<F3DCharacter>();
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

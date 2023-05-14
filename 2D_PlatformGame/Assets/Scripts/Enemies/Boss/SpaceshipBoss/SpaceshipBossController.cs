using System.Collections;
using UnityEngine;

public class SpaceshipBossController : Boss
{
    public float movementSpeed = 5f; // prêdkoœæ poruszania siê bossa
    public float leftBoundary = -10f; // lewa granica ruchu bossa
    public float rightBoundary = 10f; // prawa granica ruchu bossa
    public float bombingCooldown = 5f; // czas cooldownu przed kolejnym zrzutem bomby
    public float missileCooldown = 1f; // czas cooldownu miêdzy strza³ami pocisków
    public GameObject missilePrefab; // prefabrykat pocisku
    public Transform missileSpawnPoint; // punkt, z którego zostan¹ wystrzelone pociski
    public GameObject bombPrefab; // prefabrykat bomby
    public Transform bombSpawnPoint; // punkt, z którego zostanie zrzucona bomba
    public GameObject terrain; // referencja do terenu

    public float startingHeight = 20f; // docelowa wysokoœæ startowa
    public float startingSpeed = 10f; // prêdkoœæ wznoszenia siê na pocz¹tku

    private bool isStarting = true; // flaga okreœlaj¹ca, czy statek kosmiczny jest w fazie startowania
    private bool isMovingLeft = true; // flaga okreœlaj¹ca, czy statek porusza siê w lewo
    private bool isBombing = false; // flaga okreœlaj¹ca, czy boss aktualnie zrzuca bombê
    private bool isShooting = false; // flaga okreœlaj¹ca, czy boss aktualnie strzela pociskami
    private float bombingTimer = 0f; // timer odliczaj¹cy czas do kolejnego zrzutu bomby
    private float missileTimer = 0f; // timer odliczaj¹cy czas do kolejnego strza³u pociskiem


    //Tymczasowe dodanie zdrowia graczowi
    public HealthStatus health;
    private BoxCollider2D boxCollider;


    protected override void Awake()
    {
        base.Awake(); //wykonanie metody Awake z klasy bazowej

        //Tymczasowe dodanie zdrowia graczowi
        health.health = 100;

        boxCollider = GetComponent<BoxCollider2D>(); // Pobranie referencji do BoxCollider2D
    }

    private void AdjustColliderHeight(float height)
    {
        // Pobierz pozycjê górnej granicy collidera
        float topPosition = boxCollider.bounds.max.y;

        // Oblicz now¹ wysokoœæ collidera
        float newHeight = height - transform.position.y + topPosition;

        // Zaktualizuj wysokoœæ collidera
        boxCollider.size = new Vector2(boxCollider.size.x, newHeight);
    }



    protected override void Update()
    {
        //Jeœli jest martwy to nic nie rób
        if (isDead)
            return;

        //obs³uga startowania
        if (isStarting)
        {
            transform.Translate(Vector2.up * startingSpeed * Time.deltaTime);

            if (transform.position.y >= startingHeight)
            {
                isStarting = false;
            }
        }
        else
        {
            Debug.Log("Wystartowano");
            

        }



        //// Obs³uga zrzutu bomby
        //if (isBombing)
        //{
        //    bombingTimer += Time.deltaTime;

        //    if (bombingTimer >= bombingCooldown)
        //    {
        //        DropBomb();
        //        bombingTimer = 0f;
        //        isBombing = false;
        //    }
        //}
        //// Obs³uga strza³u pociskami
        //else if (isShooting)
        //{
        //    missileTimer += Time.deltaTime;

        //    if (missileTimer >= missileCooldown)
        //    {
        //        ShootMissile();
        //        missileTimer = 0f;
        //    }
        //}
        //// Obs³uga poruszania siê bossa w lewo i prawo
        //else
        //{
        //    float movement = isMovingLeft ? -1f : 1f;
        //    transform.Translate(Vector2.right * movement * movementSpeed * Time.deltaTime);

        //    if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
        //    {
        //        isMovingLeft = !isMovingLeft;
        //        ChangeDirection();
        //    }
        //}
    }

    private void DropBomb()
    {
        Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
        Debug.Log("Spuszczam bombê");
    }

    private void ShootMissile()
    {
        Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
        Debug.Log("Strza³");
    }

    private void ChangeDirection()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBombing = true;
        }
        else if (collision.gameObject.CompareTag("Terrain"))
        {
            isShooting = true;
            StartCoroutine(ShootSeries());
        }
    }
    private IEnumerator ShootSeries()
    {
        // Zni¿anie siê do wysokoœci terraina
        while (transform.position.y > terrain.transform.position.y)
        {
            transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Rozpoczynam seriê strza³ów");

        // Symulacja serii strza³ów
        for (int i = 0; i < 5; i++)
        {
            ShootMissile();
            yield return new WaitForSeconds(missileCooldown);
        }

        Debug.Log("Koniec serii strza³ów");

        // Powrót na wy¿sz¹ wysokoœæ
        while (transform.position.y < 10f)
        {
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
            yield return null;
        }

        isShooting = false;
    }
}

using System.Collections;
using UnityEngine;

public class SpaceshipBossController : Boss
{
    public float movementSpeed = 5f; // pr�dko�� poruszania si� bossa
    public float leftBoundary = -10f; // lewa granica ruchu bossa
    public float rightBoundary = 10f; // prawa granica ruchu bossa
    public float bombingCooldown = 5f; // czas cooldownu przed kolejnym zrzutem bomby
    public float missileCooldown = 1f; // czas cooldownu mi�dzy strza�ami pocisk�w
    public GameObject missilePrefab; // prefabrykat pocisku
    public Transform missileSpawnPoint; // punkt, z kt�rego zostan� wystrzelone pociski
    public GameObject bombPrefab; // prefabrykat bomby
    public Transform bombSpawnPoint; // punkt, z kt�rego zostanie zrzucona bomba
    public GameObject terrain; // referencja do terenu

    public float startingHeight = 20f; // docelowa wysoko�� startowa
    public float startingSpeed = 10f; // pr�dko�� wznoszenia si� na pocz�tku

    private bool isStarting = true; // flaga okre�laj�ca, czy statek kosmiczny jest w fazie startowania
    private bool isMovingLeft = true; // flaga okre�laj�ca, czy statek porusza si� w lewo
    private bool isBombing = false; // flaga okre�laj�ca, czy boss aktualnie zrzuca bomb�
    private bool isShooting = false; // flaga okre�laj�ca, czy boss aktualnie strzela pociskami
    private float bombingTimer = 0f; // timer odliczaj�cy czas do kolejnego zrzutu bomby
    private float missileTimer = 0f; // timer odliczaj�cy czas do kolejnego strza�u pociskiem


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
        // Pobierz pozycj� g�rnej granicy collidera
        float topPosition = boxCollider.bounds.max.y;

        // Oblicz now� wysoko�� collidera
        float newHeight = height - transform.position.y + topPosition;

        // Zaktualizuj wysoko�� collidera
        boxCollider.size = new Vector2(boxCollider.size.x, newHeight);
    }



    protected override void Update()
    {
        //Je�li jest martwy to nic nie r�b
        if (isDead)
            return;

        //obs�uga startowania
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



        //// Obs�uga zrzutu bomby
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
        //// Obs�uga strza�u pociskami
        //else if (isShooting)
        //{
        //    missileTimer += Time.deltaTime;

        //    if (missileTimer >= missileCooldown)
        //    {
        //        ShootMissile();
        //        missileTimer = 0f;
        //    }
        //}
        //// Obs�uga poruszania si� bossa w lewo i prawo
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
        Debug.Log("Spuszczam bomb�");
    }

    private void ShootMissile()
    {
        Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
        Debug.Log("Strza�");
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
        // Zni�anie si� do wysoko�ci terraina
        while (transform.position.y > terrain.transform.position.y)
        {
            transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Rozpoczynam seri� strza��w");

        // Symulacja serii strza��w
        for (int i = 0; i < 5; i++)
        {
            ShootMissile();
            yield return new WaitForSeconds(missileCooldown);
        }

        Debug.Log("Koniec serii strza��w");

        // Powr�t na wy�sz� wysoko��
        while (transform.position.y < 10f)
        {
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
            yield return null;
        }

        isShooting = false;
    }
}

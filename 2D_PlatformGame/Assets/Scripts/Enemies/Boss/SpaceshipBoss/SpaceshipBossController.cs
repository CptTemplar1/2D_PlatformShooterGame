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
    private bool isMovingLeft = true; // flaga okre�laj�ca, czy statek porusza si� w lewo (do zmiany kierunku)
    private bool isBombing = false; // flaga okre�laj�ca, czy boss aktualnie zrzuca bomb�
    private bool isShooting = false; // flaga okre�laj�ca, czy boss aktualnie strzela pociskami
    private float bombingTimer = 0f; // timer odliczaj�cy czas do kolejnego zrzutu bomby
    private float missileTimer = 0f; // timer odliczaj�cy czas do kolejnego strza�u pociskiem

    private bool isFlyingLeftRight = false; //flaga okre�laj�ca czy statek jest w trakcie latania lewo/prawo
    private bool canDropBomb = true; //flaga okre�laj�ca czy statek mo�e zrzuci� bomb�



    //Tymczasowe dodanie zdrowia graczowi
    public HealthStatus health;


    protected override void Awake()
    {
        base.Awake(); //wykonanie metody Awake z klasy bazowej

        //Tymczasowe dodanie zdrowia graczowi
        health.health = 100;

        //DODAJ AUTOMATYCZNE DOSTOSOWANIE WIELKO�CI BOXCOLLIDERA2D NA PODSTAWIE ZMIENNEJ StartingHeight
    }



    protected override void Update()
    {
        //Je�li jest martwy to nic nie r�b
        if (isDead)
            return;

        //obs�uga startowania
        if (isStarting)
        {
            transform.Translate(startingSpeed * Time.deltaTime * Vector2.up);

            if (transform.position.y >= startingHeight)
            {
                isStarting = false;
                isFlyingLeftRight = true;
            }
        }
        //obs�uga zachowania po wystartowaniu
        else 
        {
            //obs�uga latania w lewo/prawo
            if (isFlyingLeftRight)
            {
                //poruszanie si� statku w lewo i prawo wed�ug wyznaczonych granic
                float movement = isMovingLeft ? -1f : 1f;
                transform.Translate(movement * movementSpeed * Time.deltaTime * Vector2.right);

                if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
                {
                    isMovingLeft = !isMovingLeft;
                    ChangeDirection();
                }
            }
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
    }

    //kolizja gracza z polem do zrzutu bomby
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDropBomb)
        {
            DropBomb();
        }
    }

    //metoda obs�uguj�ca zrzut bomby przez statek kosmiczny
    private void DropBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, bombSpawnPoint);
        bomb.transform.SetParent(null); // Ustawienie rodzica na null, aby bomby by�y niezale�ne
        canDropBomb = false; // Ustawienie flagi na false, aby zablokowa� kolejne zrzuty

        Invoke("ResetBombCooldown", 3f); // Zresetowanie czsu zrzutu bomby po okre�lonym czasie
    }

    //metoda resetuj�ca flag� od zrzucania bomby (statek mo�e zrzuci� kolejn�)
    private void ResetBombCooldown()
    {
        canDropBomb = true; // Ustawienie flagi na true, aby umo�liwi� zrzut kolejnej bomby
    }

    //metoda zmiany kierunku ruchu statku kosmicznego
    private void ChangeDirection()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }




    private void ShootMissile()
    {
        Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
        Debug.Log("Strza�");
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isBombing = true;
    //    }
    //    else if (collision.gameObject.CompareTag("Terrain"))
    //    {
    //        isShooting = true;
    //        StartCoroutine(ShootSeries());
    //    }
    //}
    //private IEnumerator ShootSeries()
    //{
    //    // Zni�anie si� do wysoko�ci terraina
    //    while (transform.position.y > terrain.transform.position.y)
    //    {
    //        transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
    //        yield return null;
    //    }

    //    Debug.Log("Rozpoczynam seri� strza��w");

    //    // Symulacja serii strza��w
    //    for (int i = 0; i < 5; i++)
    //    {
    //        ShootMissile();
    //        yield return new WaitForSeconds(missileCooldown);
    //    }

    //    Debug.Log("Koniec serii strza��w");

    //    // Powr�t na wy�sz� wysoko��
    //    while (transform.position.y < 10f)
    //    {
    //        transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
    //        yield return null;
    //    }

    //    isShooting = false;
    //}
}

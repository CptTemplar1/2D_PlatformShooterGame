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
    private bool isMovingLeft = true; // flaga okreœlaj¹ca, czy statek porusza siê w lewo (do zmiany kierunku)
    private bool isBombing = false; // flaga okreœlaj¹ca, czy boss aktualnie zrzuca bombê
    private bool isShooting = false; // flaga okreœlaj¹ca, czy boss aktualnie strzela pociskami
    private float bombingTimer = 0f; // timer odliczaj¹cy czas do kolejnego zrzutu bomby
    private float missileTimer = 0f; // timer odliczaj¹cy czas do kolejnego strza³u pociskiem

    private bool isFlyingLeftRight = false; //flaga okreœlaj¹ca czy statek jest w trakcie latania lewo/prawo
    private bool canDropBomb = true; //flaga okreœlaj¹ca czy statek mo¿e zrzuciæ bombê



    //Tymczasowe dodanie zdrowia graczowi
    public HealthStatus health;


    protected override void Awake()
    {
        base.Awake(); //wykonanie metody Awake z klasy bazowej

        //Tymczasowe dodanie zdrowia graczowi
        health.health = 100;

        //DODAJ AUTOMATYCZNE DOSTOSOWANIE WIELKOŒCI BOXCOLLIDERA2D NA PODSTAWIE ZMIENNEJ StartingHeight
    }



    protected override void Update()
    {
        //Jeœli jest martwy to nic nie rób
        if (isDead)
            return;

        //obs³uga startowania
        if (isStarting)
        {
            transform.Translate(startingSpeed * Time.deltaTime * Vector2.up);

            if (transform.position.y >= startingHeight)
            {
                isStarting = false;
                isFlyingLeftRight = true;
            }
        }
        //obs³uga zachowania po wystartowaniu
        else 
        {
            //obs³uga latania w lewo/prawo
            if (isFlyingLeftRight)
            {
                //poruszanie siê statku w lewo i prawo wed³ug wyznaczonych granic
                float movement = isMovingLeft ? -1f : 1f;
                transform.Translate(movement * movementSpeed * Time.deltaTime * Vector2.right);

                if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
                {
                    isMovingLeft = !isMovingLeft;
                    ChangeDirection();
                }
            }
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
    }

    //kolizja gracza z polem do zrzutu bomby
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDropBomb)
        {
            DropBomb();
        }
    }

    //metoda obs³uguj¹ca zrzut bomby przez statek kosmiczny
    private void DropBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, bombSpawnPoint);
        bomb.transform.SetParent(null); // Ustawienie rodzica na null, aby bomby by³y niezale¿ne
        canDropBomb = false; // Ustawienie flagi na false, aby zablokowaæ kolejne zrzuty

        Invoke("ResetBombCooldown", 3f); // Zresetowanie czsu zrzutu bomby po okreœlonym czasie
    }

    //metoda resetuj¹ca flagê od zrzucania bomby (statek mo¿e zrzuciæ kolejn¹)
    private void ResetBombCooldown()
    {
        canDropBomb = true; // Ustawienie flagi na true, aby umo¿liwiæ zrzut kolejnej bomby
    }

    //metoda zmiany kierunku ruchu statku kosmicznego
    private void ChangeDirection()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }




    private void ShootMissile()
    {
        Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
        Debug.Log("Strza³");
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
    //    // Zni¿anie siê do wysokoœci terraina
    //    while (transform.position.y > terrain.transform.position.y)
    //    {
    //        transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
    //        yield return null;
    //    }

    //    Debug.Log("Rozpoczynam seriê strza³ów");

    //    // Symulacja serii strza³ów
    //    for (int i = 0; i < 5; i++)
    //    {
    //        ShootMissile();
    //        yield return new WaitForSeconds(missileCooldown);
    //    }

    //    Debug.Log("Koniec serii strza³ów");

    //    // Powrót na wy¿sz¹ wysokoœæ
    //    while (transform.position.y < 10f)
    //    {
    //        transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
    //        yield return null;
    //    }

    //    isShooting = false;
    //}
}

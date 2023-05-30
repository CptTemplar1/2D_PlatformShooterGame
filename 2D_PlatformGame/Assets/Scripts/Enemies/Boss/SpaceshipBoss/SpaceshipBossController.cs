using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SpaceshipBossController : Boss
{
    public float movementSpeed = 5f; // prêdkoœæ poruszania siê bossa
    public float leftBoundary = -25f; // lewa granica ruchu bossa
    public float rightBoundary = 25f; // prawa granica ruchu bossa
    public float bombingCooldown = 5f; // czas cooldownu przed kolejnym zrzutem bomby
    public float missileCooldown = 1f; // czas cooldownu miêdzy strza³ami pocisków
    public float bulletChargeCooldown = 15f; // czas cooldownu ataku seri¹
    public GameObject missilePrefab; // prefabrykat pocisku
    public Transform missileSpawnPoint; // punkt, z którego zostan¹ wystrzelone pociski
    public Transform chargeAttackSpawnPoint; // punkt, z którego zostan¹ wystrzelone pociski podczas specjalnego ataku
    public GameObject bombPrefab; // prefabrykat bomby
    public Transform bombSpawnPoint; // punkt, z którego zostanie zrzucona bomba
    public GameObject terrain; // referencja do terenu
    public LayerMask ignoreLayer; //maska zawieraj¹ca warstwê bossa, ¿eby jego raycast w niego nie trafia³

    public Transform bulletFirePoint; //miejsce wystrza³u zwyk³ego pocisku
    public GameObject bulletPrefab; //prefab zwyk³ego pocisku

    public float startingHeight = 20f; // docelowa wysokoœæ startowa
    public float startingSpeed = 10f; // prêdkoœæ wznoszenia siê na pocz¹tku

    private bool isStarting = true; // flaga okreœlaj¹ca, czy statek kosmiczny jest w fazie startowania
    private bool isMovingLeft = true; // flaga okreœlaj¹ca, czy statek porusza siê w lewo (do zmiany kierunku)
    private bool isFlying = false; //flaga okreœlaj¹ca czy statek jest w trakcie latania lewo/prawo
    private bool canDropBomb = false; //flaga okreœlaj¹ca czy statek mo¿e zrzuciæ bombê
    private bool isBulletChargeActive = false; //flaga okreœlaj¹ca czy helikopter wykonuje aktualnie szar¿ê pocisków (Zni¿enie siê i du¿a salwa)
    private bool hasPerformedBulletCharge = false; // Flaga informuj¹ca, czy metoda obs³ugi szar¿y pocisków zosta³a ju¿ wywo³ana (zapobiega wielokrotnemu wywo³aniu)
    private bool isChangingDirection = false; //flaga okreœlaj¹ca czy statek w³aœnie nie zmieni³ kierunku lotu, dziêki niej statek nie buguje siê i nie zmienia kierunku wiele razy

    private Transform player;

    protected override void Awake()
    {
        base.Awake(); //wykonanie metody Awake z klasy bazowej

        //TODO: DODAJ AUTOMATYCZNE DOSTOSOWANIE WIELKOŒCI BOXCOLLIDERA2D NA PODSTAWIE ZMIENNEJ StartingHeight
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
                //zakoñczenie startu i rozpoczêcie ruchu lewo/prawo
                isStarting = false;
                isFlying = true;
                canDropBomb = true;

                //uruchomienie atakowania gracza po wystartowaniu
                StartCoroutine(ShootBulletCoroutine());
                StartCoroutine(ShootMissileCoroutine());
                StartCoroutine(PerformBulletChargeCoroutine());
            }
        }
        //obs³uga zachowania po wystartowaniu
        else 
        {
            //obs³uga latania w lewo/prawo
            if (isFlying)
            {
                // poruszanie siê statku w lewo i prawo wed³ug wyznaczonych granic
                float movement = isMovingLeft ? -1f : 1f;
                transform.Translate(movement * movementSpeed * Time.deltaTime * Vector2.right);

                if (!isChangingDirection && (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary))
                {
                    // Sprawdzanie, czy statek nie jest ju¿ w trakcie zmiany kierunku
                    isChangingDirection = true;

                    // Odwracanie kierunku tylko wtedy, gdy statek nie jest ju¿ w granicy
                    if ((transform.position.x <= leftBoundary && isMovingLeft) || (transform.position.x >= rightBoundary && !isMovingLeft))
                    {
                        isMovingLeft = !isMovingLeft;
                        ChangeDirection();
                    }
                }
                else if (isChangingDirection && transform.position.x > leftBoundary && transform.position.x < rightBoundary)
                {
                    // Resetowanie zmiennej isChangingDirection, gdy statek opuœci granicê
                    isChangingDirection = false;
                }
            }
            //obs³uga szar¿y pocisków
            else if (isBulletChargeActive)
            {
                //pojedyncze wykonanie metody odwrócenia statku, przez co nie zmienia ju¿ kierunku po pierwszym zwrocie
                if (!isChangingDirection)
                {
                    ChangeDirectrionToPlayer();
                }
                //rzucamy RayCasta pionowo w dó³, ¿eby znaleŸæ miejsce do l¹dowania
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, ~ignoreLayer);
                if (hit.collider != null)
                {
                    float terrainHeight = hit.point.y;
                    float targetHeight = terrainHeight + 3f; // podnosimy lekko docelow¹ wysokoœæ, bo transform statku jest na jego œrodku, przez co statek wpad³by w pod³ogê
                    float currentHeight = transform.position.y;

                    // Zmniejszaj wysokoœæ statku, jeœli jest wy¿ej ni¿ docelowa wysokoœæ
                    if (currentHeight > targetHeight)
                    {
                        float newY = Mathf.MoveTowards(currentHeight, targetHeight, startingSpeed * Time.deltaTime);
                        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                    }
                    //jeœli statek jest ju¿ nad ziemi¹
                    else
                    {
                        if (!hasPerformedBulletCharge)
                        {
                            hasPerformedBulletCharge = true;
                            Invoke("PerformBulletCharge", 2f); // Wywo³anie metody po 2 sekundach od wyl¹dowania
                        }
                    }
                }
            }
        }
    }

    //metoda obs³uguj¹ca zrzut bomby przez statek kosmiczny
    public void DropBomb()
    {
        //jeœli cooldown zrzutu bomby ju¿ up³yn¹³, to zrzuæ bombê
        if (canDropBomb)
        {
            GameObject bomb = Instantiate(bombPrefab, bombSpawnPoint);
            bomb.transform.SetParent(null); // Ustawienie rodzica na null, aby bomby by³y niezale¿ne
            canDropBomb = false; // Ustawienie flagi na false, aby zablokowaæ kolejne zrzuty

            Invoke("ResetBombCooldown", 3f); // Zresetowanie czsu zrzutu bomby po okreœlonym czasie
        }
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

    //metoda zmiany kierunku ruchu statku kosmicznego w taki sposób, aby by³ skierowany frontem do gracza
    private void ChangeDirectrionToPlayer()
    {
        // Odwrócenie w przypadku, gdy gracz znajduje siê po lewej stronie statku
        if (player.position.x < transform.position.x && !isMovingLeft)
        {
            ChangeDirection();
            isMovingLeft = true;
            isChangingDirection = true;
        }
        // Odwrócenie w przypadku, gdy gracz znajduje siê po prawej stronie statku
        else if (player.position.x > transform.position.x && isMovingLeft)
        {
            ChangeDirection();
            isMovingLeft = false;
            isChangingDirection = true;
        }
    }

    //metoda obs³ugi wystrza³u szar¿y pocisków po zni¿eniu siê statku kosmicznego
    private void PerformBulletCharge()
    {
        for(int i = 0; i<10; i++)
        {
            Debug.Log("STRZA£ Z SZAR¯Y POCISKÓW");
            StartCoroutine(ShootBulletChargeCoroutine());
        }
        Invoke("RestartBehaviour", 2f);
    }

    //strzelanie do gracza zwyk³ym pociskiem w trakcie ataku specjalnego
    private IEnumerator ShootBulletChargeCoroutine()
    {
        while (true)
        {
            Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation);
            yield return new WaitForSeconds(0.3f);
        }
    }

    //metoda restartuj¹ca flagi, aby helikopter znowu rozpcozyna³ swoje zachowanie od startowania
    private void RestartBehaviour()
    {
        isStarting = true; //po zakoñczeniu ataku wracamy do punktu wyjœcia, czyli do startowania (bo statek jest znowu na ziemi)
        isChangingDirection = false;
        hasPerformedBulletCharge = false;
    }


    //strzelanie do gracza zwyk³ym pociskiem co okreœlony czas
    private IEnumerator ShootBulletCoroutine()
    {
        while (true)
        {
            Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation);
            yield return new WaitForSeconds(missileCooldown);
        }
    }

    //strzelanie do gracza rakiet¹ co 5 sekund
    private IEnumerator ShootMissileCoroutine()
    {
        while (true)
        {
            Debug.Log("Strzelam Rakiet¹");
            yield return new WaitForSeconds(bombingCooldown);
        }
    }

    //specjalny atak zni¿ania siê helikoptera do poziomu ziemi i strzelania w niego seri¹ pocisków
    //raz na 15 sekund
    private IEnumerator PerformBulletChargeCoroutine()
    {
        yield return new WaitForSeconds(bulletChargeCooldown);
        isFlying = false;
        canDropBomb = false;
        isBulletChargeActive = true;
        StopAllCoroutines();
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SpaceshipBossController : Boss
{
    public float movementSpeed = 5f; // pr�dko�� poruszania si� bossa
    public float leftBoundary = -25f; // lewa granica ruchu bossa
    public float rightBoundary = 25f; // prawa granica ruchu bossa
    public float bombingCooldown = 5f; // czas cooldownu przed kolejnym zrzutem bomby
    public float missileCooldown = 1f; // czas cooldownu mi�dzy strza�ami pocisk�w
    public float bulletChargeCooldown = 15f; // czas cooldownu ataku seri�
    public GameObject missilePrefab; // prefabrykat pocisku
    public Transform missileSpawnPoint; // punkt, z kt�rego zostan� wystrzelone pociski
    public Transform chargeAttackSpawnPoint; // punkt, z kt�rego zostan� wystrzelone pociski podczas specjalnego ataku
    public GameObject bombPrefab; // prefabrykat bomby
    public Transform bombSpawnPoint; // punkt, z kt�rego zostanie zrzucona bomba
    public GameObject terrain; // referencja do terenu
    public LayerMask ignoreLayer; //maska zawieraj�ca warstw� bossa, �eby jego raycast w niego nie trafia�

    public Transform bulletFirePoint; //miejsce wystrza�u zwyk�ego pocisku
    public GameObject bulletPrefab; //prefab zwyk�ego pocisku

    public float startingHeight = 20f; // docelowa wysoko�� startowa
    public float startingSpeed = 10f; // pr�dko�� wznoszenia si� na pocz�tku

    private bool isStarting = true; // flaga okre�laj�ca, czy statek kosmiczny jest w fazie startowania
    private bool isMovingLeft = true; // flaga okre�laj�ca, czy statek porusza si� w lewo (do zmiany kierunku)
    private bool isFlying = false; //flaga okre�laj�ca czy statek jest w trakcie latania lewo/prawo
    private bool canDropBomb = false; //flaga okre�laj�ca czy statek mo�e zrzuci� bomb�
    private bool isBulletChargeActive = false; //flaga okre�laj�ca czy helikopter wykonuje aktualnie szar�� pocisk�w (Zni�enie si� i du�a salwa)
    private bool hasPerformedBulletCharge = false; // Flaga informuj�ca, czy metoda obs�ugi szar�y pocisk�w zosta�a ju� wywo�ana (zapobiega wielokrotnemu wywo�aniu)
    private bool isChangingDirection = false; //flaga okre�laj�ca czy statek w�a�nie nie zmieni� kierunku lotu, dzi�ki niej statek nie buguje si� i nie zmienia kierunku wiele razy

    private Transform player;

    protected override void Awake()
    {
        base.Awake(); //wykonanie metody Awake z klasy bazowej

        //TODO: DODAJ AUTOMATYCZNE DOSTOSOWANIE WIELKO�CI BOXCOLLIDERA2D NA PODSTAWIE ZMIENNEJ StartingHeight
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
                //zako�czenie startu i rozpocz�cie ruchu lewo/prawo
                isStarting = false;
                isFlying = true;
                canDropBomb = true;

                //uruchomienie atakowania gracza po wystartowaniu
                StartCoroutine(ShootBulletCoroutine());
                StartCoroutine(ShootMissileCoroutine());
                StartCoroutine(PerformBulletChargeCoroutine());
            }
        }
        //obs�uga zachowania po wystartowaniu
        else 
        {
            //obs�uga latania w lewo/prawo
            if (isFlying)
            {
                // poruszanie si� statku w lewo i prawo wed�ug wyznaczonych granic
                float movement = isMovingLeft ? -1f : 1f;
                transform.Translate(movement * movementSpeed * Time.deltaTime * Vector2.right);

                if (!isChangingDirection && (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary))
                {
                    // Sprawdzanie, czy statek nie jest ju� w trakcie zmiany kierunku
                    isChangingDirection = true;

                    // Odwracanie kierunku tylko wtedy, gdy statek nie jest ju� w granicy
                    if ((transform.position.x <= leftBoundary && isMovingLeft) || (transform.position.x >= rightBoundary && !isMovingLeft))
                    {
                        isMovingLeft = !isMovingLeft;
                        ChangeDirection();
                    }
                }
                else if (isChangingDirection && transform.position.x > leftBoundary && transform.position.x < rightBoundary)
                {
                    // Resetowanie zmiennej isChangingDirection, gdy statek opu�ci granic�
                    isChangingDirection = false;
                }
            }
            //obs�uga szar�y pocisk�w
            else if (isBulletChargeActive)
            {
                //pojedyncze wykonanie metody odwr�cenia statku, przez co nie zmienia ju� kierunku po pierwszym zwrocie
                if (!isChangingDirection)
                {
                    ChangeDirectrionToPlayer();
                }
                //rzucamy RayCasta pionowo w d�, �eby znale�� miejsce do l�dowania
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, ~ignoreLayer);
                if (hit.collider != null)
                {
                    float terrainHeight = hit.point.y;
                    float targetHeight = terrainHeight + 3f; // podnosimy lekko docelow� wysoko��, bo transform statku jest na jego �rodku, przez co statek wpad�by w pod�og�
                    float currentHeight = transform.position.y;

                    // Zmniejszaj wysoko�� statku, je�li jest wy�ej ni� docelowa wysoko��
                    if (currentHeight > targetHeight)
                    {
                        float newY = Mathf.MoveTowards(currentHeight, targetHeight, startingSpeed * Time.deltaTime);
                        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                    }
                    //je�li statek jest ju� nad ziemi�
                    else
                    {
                        if (!hasPerformedBulletCharge)
                        {
                            hasPerformedBulletCharge = true;
                            Invoke("PerformBulletCharge", 2f); // Wywo�anie metody po 2 sekundach od wyl�dowania
                        }
                    }
                }
            }
        }
    }

    //metoda obs�uguj�ca zrzut bomby przez statek kosmiczny
    public void DropBomb()
    {
        //je�li cooldown zrzutu bomby ju� up�yn��, to zrzu� bomb�
        if (canDropBomb)
        {
            GameObject bomb = Instantiate(bombPrefab, bombSpawnPoint);
            bomb.transform.SetParent(null); // Ustawienie rodzica na null, aby bomby by�y niezale�ne
            canDropBomb = false; // Ustawienie flagi na false, aby zablokowa� kolejne zrzuty

            Invoke("ResetBombCooldown", 3f); // Zresetowanie czsu zrzutu bomby po okre�lonym czasie
        }
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

    //metoda zmiany kierunku ruchu statku kosmicznego w taki spos�b, aby by� skierowany frontem do gracza
    private void ChangeDirectrionToPlayer()
    {
        // Odwr�cenie w przypadku, gdy gracz znajduje si� po lewej stronie statku
        if (player.position.x < transform.position.x && !isMovingLeft)
        {
            ChangeDirection();
            isMovingLeft = true;
            isChangingDirection = true;
        }
        // Odwr�cenie w przypadku, gdy gracz znajduje si� po prawej stronie statku
        else if (player.position.x > transform.position.x && isMovingLeft)
        {
            ChangeDirection();
            isMovingLeft = false;
            isChangingDirection = true;
        }
    }

    //metoda obs�ugi wystrza�u szar�y pocisk�w po zni�eniu si� statku kosmicznego
    private void PerformBulletCharge()
    {
        for(int i = 0; i<10; i++)
        {
            Debug.Log("STRZA� Z SZAR�Y POCISK�W");
            StartCoroutine(ShootBulletChargeCoroutine());
        }
        Invoke("RestartBehaviour", 2f);
    }

    //strzelanie do gracza zwyk�ym pociskiem w trakcie ataku specjalnego
    private IEnumerator ShootBulletChargeCoroutine()
    {
        while (true)
        {
            Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation);
            yield return new WaitForSeconds(0.3f);
        }
    }

    //metoda restartuj�ca flagi, aby helikopter znowu rozpcozyna� swoje zachowanie od startowania
    private void RestartBehaviour()
    {
        isStarting = true; //po zako�czeniu ataku wracamy do punktu wyj�cia, czyli do startowania (bo statek jest znowu na ziemi)
        isChangingDirection = false;
        hasPerformedBulletCharge = false;
    }


    //strzelanie do gracza zwyk�ym pociskiem co okre�lony czas
    private IEnumerator ShootBulletCoroutine()
    {
        while (true)
        {
            Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation);
            yield return new WaitForSeconds(missileCooldown);
        }
    }

    //strzelanie do gracza rakiet� co 5 sekund
    private IEnumerator ShootMissileCoroutine()
    {
        while (true)
        {
            Debug.Log("Strzelam Rakiet�");
            yield return new WaitForSeconds(bombingCooldown);
        }
    }

    //specjalny atak zni�ania si� helikoptera do poziomu ziemi i strzelania w niego seri� pocisk�w
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

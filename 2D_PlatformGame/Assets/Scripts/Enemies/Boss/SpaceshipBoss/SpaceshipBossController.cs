using System;
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
    public float bulletCooldown = 1f; // czas cooldownu miêdzy strza³ami pocisków
    public float bulletChargeCooldown = 15f; // czas cooldownu ataku seri¹
    public float missileCooldown = 5f; //czas cooldownu strzelania rakiet¹ samonaprowadzan¹
    
    public GameObject missilePrefab; // prefabrykat pocisku
    public Transform missileSpawnPoint; // punkt, z którego zostan¹ wystrzelone pociski
    
    public Transform chargeAttackSpawnPoint; // punkt, z którego zostan¹ wystrzelone pociski podczas specjalnego ataku
    public GameObject chargeAttackBullet; //obiekt pocisku lec¹cego prosto
    public AudioSource chargeAttackChargingSource; //Ÿród³o dŸwiêku ³adowania szar¿y pocisków
    public AudioSource chargeAttackCannonSource; //Ÿród³o dŸwiêku wystrza³u podczas szar¿y pocisków
    public AudioClip chargeAttackBulletClip; //klip podczas wystrza³u z dzia³a podczas szar¿y pocisków

    public GameObject bombPrefab; // prefabrykat bomby
    public Transform bombSpawnPoint; // punkt, z którego zostanie zrzucona bomba
    public GameObject terrain; // referencja do terenu
    public LayerMask ignoreLayer; //maska zawieraj¹ca warstwê bossa, ¿eby jego raycast w niego nie trafia³

    public AudioSource cannonAudioSource; //Ÿród³o dŸwiêku strzelania z g³ównego dzia³a
    public AudioClip cannonAudioClip; //klip odtwarzany przy strzelaniu z g³ównego dzia³a

    public Transform bulletFirePoint; //miejsce wystrza³u zwyk³ego pocisku
    public GameObject bulletPrefab; //prefab zwyk³ego pocisku
    public GameObject muzzleFlash; //obiekt zawieraj¹cy system cz¹steczek wystrza³u

    public float startingHeight = 20f; // docelowa wysokoœæ startowa
    public float startingSpeed = 10f; // prêdkoœæ wznoszenia siê na pocz¹tku

    public AudioSource startingSoundSource; //Ÿród³o dŸwiêków startowania bossa
    public AudioSource flyingSoundSource; //Ÿród³o dŸwiêków latania bossa
    private bool hasPlayedStartingSound = false; //flaga okreœlaj¹ca czy czy dŸwiêk startowania zosta³ ju¿ odtworzony
    private bool hasPlayedFlyingSound = false; //flaga okreœlaj¹ca czy czy dŸwiêk latania zosta³ ju¿ odtworzony

    public AudioSource deathSoundSource; //Ÿród³o dŸwiêku spadania po œmierci
    public GameObject deathExplosions; //obiekt przechowuj¹cy eksplozje statku, które maj¹ pojawiæ siê dopiero po œmierci

    private bool isStarting = true; // flaga okreœlaj¹ca, czy statek kosmiczny jest w fazie startowania
    private bool isMovingLeft = true; // flaga okreœlaj¹ca, czy statek porusza siê w lewo (do zmiany kierunku)
    private bool isFlying = false; //flaga okreœlaj¹ca czy statek jest w trakcie latania lewo/prawo
    private bool canDropBomb = false; //flaga okreœlaj¹ca czy statek mo¿e zrzuciæ bombê
    private bool isBulletChargeActive = false; //flaga okreœlaj¹ca czy helikopter wykonuje aktualnie szar¿ê pocisków (Zni¿enie siê i du¿a salwa)
    private bool hasPerformedBulletCharge = false; // Flaga informuj¹ca, czy metoda obs³ugi szar¿y pocisków zosta³a ju¿ wywo³ana (zapobiega wielokrotnemu wywo³aniu)
    private bool isChangingDirection = false; //flaga okreœlaj¹ca czy statek w³aœnie nie zmieni³ kierunku lotu, dziêki niej statek nie buguje siê i nie zmienia kierunku wiele razy
    private bool hasFallen = false;  //flaga okreœlaj¹ca czy helikopter po œmierci spad³ ju¿ na ziemiê

    public GameObject player; //obiekt gracza
    public F3DCharacter playerCharacter; //komponent gracza

    protected override void Awake()
    {
        base.Awake(); //wykonanie metody Awake z klasy bazowej

        //TODO: DODAJ AUTOMATYCZNE DOSTOSOWANIE WIELKOŒCI BOXCOLLIDERA2D NA PODSTAWIE ZMIENNEJ StartingHeight
    }

    protected override void Update()
    {
        //jeœli gracz jest martwy to przestañ do niego strzelaæ
        if (playerCharacter._isDead)
        {
            StopSounds();
            canDropBomb = false;
            isBulletChargeActive = false;
            StopAllCoroutines();
        }
        
        //Jeœli jest martwy to nic nie rób
        if (isDead)
        {
            //obs³uga spadania statku kosmicznego na ziemiê po zestrzeleniu
            if (!hasFallen)
            {
                //rzucamy RayCasta pionowo w dó³, ¿eby znaleŸæ miejsce na ziemi
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, ~ignoreLayer);
                if (hit.collider != null)
                {
                    float terrainHeight = hit.point.y;
                    float targetHeight = terrainHeight + 1f; // podnosimy lekko docelow¹ wysokoœæ, bo transform statku jest na jego œrodku, przez co statek wpad³by w pod³ogê
                    float currentHeight = transform.position.y;

                    // Zmniejszaj wysokoœæ statku, jeœli jest wy¿ej ni¿ docelowa wysokoœæ
                    if (currentHeight > targetHeight)
                    {
                        float newY = Mathf.MoveTowards(currentHeight, targetHeight, startingSpeed * Time.deltaTime);
                        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                    }
                    //jeœli statek jest ju¿ na ziemi
                    else
                    {
                        hasFallen = true;
                        StopCoroutine(ChangeDirectionWhileFalling()); //wy³¹czenie obracania po spadniêciu na ziemiê
                    }
                }
            }


            return;
        }
        else
        {
            //obs³uga startowania
            if (isStarting)
            {
                //odtworzenie dŸwiêku startu tylko raz
                if (!hasPlayedStartingSound)
                {
                    startingSoundSource.Play(); // odtworzenie dŸwiêku startu statku
                    hasPlayedStartingSound = true; // ustawienie flagi na true po odtworzeniu dŸwiêku
                }

                transform.Translate(startingSpeed * Time.deltaTime * Vector2.up);

                if (transform.position.y >= startingHeight)
                {
                    //zakoñczenie startu i rozpoczêcie ruchu lewo/prawo
                    isStarting = false;
                    isFlying = true;
                    canDropBomb = true;

                    hasPlayedStartingSound = false;

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
                    //w³¹czenie odtwarzania dŸwiêku latania
                    if (!hasPlayedFlyingSound)
                    {
                        flyingSoundSource.Play();
                        hasPlayedFlyingSound = true;
                    }
                    
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
                    flyingSoundSource.Stop(); //zastopowanie dŸwiêku latania podczas przeprowadzania ataku
                    hasPlayedFlyingSound = false;

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

                                chargeAttackChargingSource.Play(); //odtworzenie dŸwiêku ³adowania szar¿y pocisków
                                Invoke("PerformBulletCharge", 2f); // Wywo³anie metody po 2 sekundach od wyl¹dowania
                            }
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
        if(hasPerformedBulletCharge != true)
        {
            canDropBomb = true; // Ustawienie flagi na true, aby umo¿liwiæ zrzut kolejnej bomby
        }
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
        if (player.transform.position.x < transform.position.x && !isMovingLeft)
        {
            ChangeDirection();
            isMovingLeft = true;
            isChangingDirection = true;
        }
        // Odwrócenie w przypadku, gdy gracz znajduje siê po prawej stronie statku
        else if (player.transform.position.x > transform.position.x && isMovingLeft)
        {
            ChangeDirection();
            isMovingLeft = false;
            isChangingDirection = true;
        }
    }

    //metoda obs³ugi wystrza³u szar¿y pocisków po zni¿eniu siê statku kosmicznego
    private void PerformBulletCharge()
    {
        float cooldown = 0.1f;
        for (int i = 0; i < 20; i++)
        {
            Invoke("SpawnChargeBullet", cooldown);
            cooldown += 0.1f;
        }
        Invoke("RestartBehaviour", 3f);
    }

    //metoda spawnuj¹ca obiekt pocisku specjalnego podczas szar¿y pocisków
    //wykorzystywana do opóŸniania pawnu przez Invoke
    private void SpawnChargeBullet()
    {
        GameObject bullet = Instantiate(chargeAttackBullet, chargeAttackSpawnPoint);
        bullet.transform.SetParent(null); // Ustawienie rodzica na null, aby pociski by³y niezale¿ne
        F3DAudio.PlayOneShotRandom(chargeAttackCannonSource, chargeAttackBulletClip, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dŸwiêku wystrza³u
    }

    //metoda restartuj¹ca flagi, aby helikopter znowu rozpoczyna³ swoje zachowanie od startowania
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
            //TODO: Poprawiæ spawnowanie siê rozb³ysku, ¿eby by³ skierowany w stronê gracza
            GameObject flash = Instantiate(muzzleFlash, bulletFirePoint.position, Quaternion.Euler(0, 0, -90)); //zespawnowanie rozb³ysku wystrza³u skierowanego wstêpnie w dó³
            Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation); //zespawnowanie pocisku lec¹cego w stronê gracza
            Destroy(flash, 5f); //usuwanie obiektu rozb³ysku po strzale po up³ywie 5 sekund

            F3DAudio.PlayOneShotRandom(cannonAudioSource, cannonAudioClip, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dŸwiêku wystrza³u

            yield return new WaitForSeconds(bulletCooldown);
        }
    }


    //strzelanie do gracza rakiet¹ co 5 sekund
    private IEnumerator ShootMissileCoroutine()
    {
        while (true)
        {
            Debug.Log("Strzelam Rakiet¹");
            yield return new WaitForSeconds(missileCooldown);
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

    //obkrêcanie siê statku kosmicznego podczas spadania
    private IEnumerator ChangeDirectionWhileFalling()
    {
        while (true)
        {
            ChangeDirection();
            yield return new WaitForSeconds(1f);
        }
    }

    //nadpisanie metody bazowej Die() o dodatkowe zatrzymanie akcji bossa - ustawienie flag i zatrzymanie odliczañ
    protected override void Die()
    {
        base.Die();
        StopAllCoroutines(); //zatrzymanie wszystkich odliczañ
        canDropBomb=false;
        isBulletChargeActive=false;
        StopSounds(); //zatrzymanie dŸwiêków po œmierci

        deathSoundSource.Play(); //odtwarzanie dŸwiêku spadania
        StartCoroutine(ChangeDirectionWhileFalling()); //w³¹czenie obracania siê statku podczas spadania po œmierci
        deathExplosions.SetActive(true); //w³¹czenie wybuchów na statku po jego œmierci
    }

    //metoda stopuj¹ca dŸwiêki bossa po zabiciu gracza
    private void StopSounds()
    {
        //zatrzymanie dŸwiêków bossa
        flyingSoundSource.Stop();
        startingSoundSource.Stop();
        cannonAudioSource.Stop();
        chargeAttackChargingSource.Stop();
        chargeAttackCannonSource.Stop();
    }
}

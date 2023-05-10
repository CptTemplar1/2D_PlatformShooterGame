using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public float runSpeed = 3f;
    public bool walkingSide = false;
    Vector2 rightPoint = new Vector2();
    Vector2 leftPoint = new Vector2();
    private bool isRunning = false;
    private bool coroutineBlocker = false;

    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool isShooting = false;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
        rightPoint = new Vector2(spawnPoint.transform.position.x + walkingDistance, spawnPoint.transform.position.y); // zamiana Transform spawnPoint na Vector2
        leftPoint = new Vector2(spawnPoint.transform.position.x - walkingDistance, spawnPoint.transform.position.y); // zamiana Transform spawnPoint na Vector2
        walkingSide = false;
        isRunning = false;
        isShooting = false;
        coroutineBlocker = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerVec = new Vector2(player.transform.position.x, player.transform.position.y + 1.0f); // przeliczenie pozycji gracza na vector2

        //jeœli gracz jest w zasiêgu wykrywania
        if (Vector2.Distance(transform.position, player.position) <= detectionRange && !Physics2D.Linecast(transform.position, playerVec, groundLayer))
        {
            animator.SetBool("IsRunning", false);

            Vector2 direction = player.position - transform.position;
            direction = new Vector2(Mathf.Sign(direction.x), 0f);

            // obracanie potwora w lewo lub prawo w zale¿noœci od kierunku, w którym znajduje siê gracz
            if (direction.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            if (isShooting == false)
            {
                isShooting = true;
                Shoot();
                StartCoroutine(Shoot());
            }
            coroutineBlocker = true;
        }
        else
        {
            if(coroutineBlocker == true)
            {
                coroutineBlocker = false;
                StartCoroutine(run());
            }

            if(isRunning == true)
            {
                animator.SetBool("IsRunning", true);

                if (Vector2.Distance(transform.position, rightPoint) >= 0 && walkingSide == false)
                {
                    Vector2 direction = new Vector2(Mathf.Sign(rightPoint.x), 0f);
                    //zmiana kierunku po dotarciu do krawedzi
                    if (Mathf.Abs(transform.position.x - rightPoint.x) <= 0.1)
                    {
                        walkingSide = true;
                        return;
                    }
                    // obracanie potwora 
                    if (transform.localScale.x < 0)
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    //poruszanie potwora
                    transform.Translate(direction * runSpeed * Time.deltaTime);
                }
                if (Vector2.Distance(transform.position, leftPoint) >= 0 && walkingSide == true)
                {
                    Vector2 direction = new Vector2(Mathf.Sign(rightPoint.x), 0f);
                    //zmiana kierunku po dotarciu do krawedzi
                    if (Mathf.Abs(transform.position.x - leftPoint.x) <= 0.1)
                    {
                        walkingSide = false;
                        return;
                    }
                    // obracanie potwora
                    if (transform.localScale.x > 0)
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    //poruszanie potwora
                    transform.Translate(-direction * runSpeed * Time.deltaTime);
                }
            }
        }
    }

    IEnumerator run()
    {
        yield return new WaitForSeconds(1.0f);
        isRunning = true;
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
        // spawnujemy pocisk na scenie w miejscu przed luf¹
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        isShooting = false;
    }

    public float getDetectionRange()
    {
        return detectionRange;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WalkingEnemyController : Enemy
{
    public float speed; // prêdkoœæ, z jak¹ potwór bêdzie siê porusza³
    public bool walkingSide = false;
    public bool isWalkingBack = false;
    public bool isPatrolling = false;
    public bool isAfterHunting = false;
    public List<GameObject> triggers = new List<GameObject>();
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
    }

    void Update()
    {
        //jeœli gracz jest w zasiêgu wykrywania
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            //zmiana parametru animatora na chodzenie
            animator.SetBool("isWalking", true);

            // okreœlenie kierunku ruchu potwora
            Vector2 direction = player.position - transform.position;
            direction = new Vector2(Mathf.Sign(direction.x), 0f);

            // obracanie potwora w lewo lub prawo w zale¿noœci od kierunku, w którym znajduje siê gracz
            if (direction.x > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            // poruszanie potwora w kierunku gracza tylko w prawo lub w lewo
            transform.Translate(direction * speed * Time.deltaTime);
            isWalkingBack = false;
            isPatrolling = false;
            isAfterHunting = true;
        }
        else
        {
            if (isAfterHunting == true && Vector2.Distance(transform.position, player.position) > detectionRange && Mathf.Abs(transform.position.x - spawnPoint.position.x) > 0.3 && Mathf.Abs(spawnPoint.position.y - transform.position.y) < 0.5)
            {
                isAfterHunting = false;
                animator.SetBool("isWalking", false);
                StartCoroutine(setIsWalkingBack());
            }
            if (isWalkingBack == true && Vector2.Distance(transform.position, player.position) > detectionRange)
            {
                getBack();
            }
            /*if(isWalkingBack == false && Vector2.Distance(transform.position, player.position) > detectionRange)
            {
                animator.SetBool("isWalking", false);
                StartCoroutine(setIsPatrolling());
            }
            if (isPatrolling == true && Vector2.Distance(transform.position, player.position) > detectionRange)
            {
                justWalk();
            }*/
        }
    }

    //metoda ktora ustawia flage powrotu dla potwora
    IEnumerator setIsWalkingBack()
    {
        yield return new WaitForSeconds(1);
        if(isWalkingBack == false) 
        {
            isWalkingBack = true;
        }
    }

    //metoda ktora ustawia flage patrolowania dla potwora
    IEnumerator setIsPatrolling()
    {
        yield return new WaitForSeconds(2);
        if(isWalkingBack == false)
        {
            isPatrolling = true;
        }
    }


    //metoda odpowiedzialna za poruszanie przeciwnika w czasie gdy nie goni gracza
    public void justWalk()
    {
        Vector2 direction = new Vector2(Mathf.Abs(spawnPoint.position.x - walkingDistance), spawnPoint.position.y);
        Vector2 minus = new Vector2(-1.0f, 0);
        Vector2 plus = new Vector2(1.0f, 0);
        animator.SetBool("isWalking", true);

        if (Mathf.Abs(transform.position.x - direction.x) >= 0 && walkingSide == false)
        {
            //zmiana kierunku po dotarciu do krawedzi
            if (Mathf.Abs(transform.position.x - direction.x) <= 0.1)
            {
                walkingSide = true;
                animator.SetBool("isWalking", false);
                return;
            }
            // obracanie potwora w lewo 
            if (direction.x > 0 && transform.localScale.x > 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //poruszanie potwora
            transform.Translate(plus * speed * Time.deltaTime);
        }
        else if(Mathf.Abs(transform.position.x - direction.x) >= 0 && walkingSide == true)
        {
            //zmiana kierunku po dotarciu do krawedzi
            if (Mathf.Abs(transform.position.x - direction.x) <= 0.1)
            {
                walkingSide = false;
                animator.SetBool("isWalking", false);
                return;
            }
            // obracanie potwora w prawo
            if (direction.x < 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //poruszanie potwora
            transform.Translate(minus * speed * Time.deltaTime);
        }    

    }
    //metoda odpowiadaj¹ca za powrót potwora na miejsce spawnu po zakonczonym ataku
    public void getBack()
    {
        if (Mathf.Abs(transform.position.x - spawnPoint.position.x) <= 0.3)
        {
            animator.SetBool("isWalking", false);
            isWalkingBack = false;
            return;
        }
        // obliczenie kierunku
        animator.SetBool("isWalking", true);
        Vector2 direction = spawnPoint.position - transform.position;
        direction = new Vector2(Mathf.Sign(direction.x), 0f);
        // obracanie potwora w lewo lub prawo w zale¿noœci od kierunku
        if (direction.x > 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        transform.Translate(direction * speed * Time.deltaTime);
    }
}


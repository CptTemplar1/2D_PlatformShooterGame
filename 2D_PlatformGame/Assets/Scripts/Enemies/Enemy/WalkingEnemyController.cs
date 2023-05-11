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
    public bool canPatrol = false;
    Vector2 LeftPoint = new Vector2();
    Vector2 RightPoint = new Vector2();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
        canPatrol = true;
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
            canPatrol = true;
        }
        else
        {
            if (isAfterHunting == true && Vector2.Distance(transform.position, player.position) > detectionRange)
            {
                animator.SetBool("isWalking", false);
            }
            if (isAfterHunting == true && Vector2.Distance(transform.position, player.position) > detectionRange && Mathf.Abs(transform.position.x - spawnPoint.position.x) > 0.1 && Mathf.Abs(spawnPoint.position.y - transform.position.y) < 0.5)
            {
                isAfterHunting = false;
                animator.SetBool("isWalking", false);
                StartCoroutine(setIsWalkingBack());
            }
            if (isWalkingBack == true && Vector2.Distance(transform.position, player.position) > detectionRange)
            {
                getBack();
            }
            if(!animator.GetBool("isWalking") && Vector2.Distance(transform.position, player.position) > detectionRange && canPatrol == true && isWalkingBack == false)
            {
                canPatrol = false;
                animator.SetBool("isWalking", false);
                StartCoroutine(setIsPatrolling());
            }
            if (isPatrolling == true && Vector2.Distance(transform.position, player.position) > detectionRange)
            {
                justWalk();
            }
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
        yield return new WaitForSeconds(1);
        if(isWalkingBack == false && !animator.GetBool("isWalking"))
        {
            LeftPoint = new Vector2(transform.position.x - walkingDistance, transform.position.y);
            RightPoint = new Vector2(transform.position.x + walkingDistance, transform.position.y);
            isPatrolling = true;
        }
    }


    //metoda odpowiedzialna za poruszanie przeciwnika w czasie gdy nie goni gracza
    public void justWalk()
    {
        animator.SetBool("isWalking", true);
        
        if (Vector2.Distance(transform.position, RightPoint) >= 0 && walkingSide == false)
        {
            Vector2 direction = new Vector2(1, 0f);
            //zmiana kierunku po dotarciu do krawedzi
            if (Mathf.Abs(transform.position.x - RightPoint.x) <= 0.1)
            {
                walkingSide = true;
                animator.SetBool("isWalking", false);
                return;
            }
            // obracanie potwora 
            if (direction.x > 0 && transform.localScale.x > 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //poruszanie potwora
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, LeftPoint) >= 0 && walkingSide == true)
        {
            Vector2 direction = new Vector2(-1, 0f);
            //zmiana kierunku po dotarciu do krawedzi
            if (Mathf.Abs(transform.position.x - LeftPoint.x) <= 0.1)
            {
                walkingSide = false;
                animator.SetBool("isWalking", false);
                return;
            }
            // obracanie potwora
            if (direction.x < 0 && transform.localScale.x < 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //poruszanie potwora
            transform.Translate(direction * speed * Time.deltaTime);
        }    

    }
    //metoda odpowiadaj¹ca za powrót potwora na miejsce spawnu po zakonczonym ataku
    public void getBack()
    {
        if (Mathf.Abs(transform.position.x - spawnPoint.position.x) <= 0.3)
        {
            animator.SetBool("isWalking", false);
            isWalkingBack = false;
            canPatrol = true;
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour 
{
    private float timer;
    public float minTime;
    public float maxTime;

    private float roarTimer; // Dodatkowy timer dla ryku potwora
    public float roarInterval = 10f; // Interwał czasowy między rykami potwora

    private Transform playerPos;
    public float speed;

    private AudioSource bossAudioSource; //źródło odtwarzania dźwięku bossa
    public AudioClip jumpSound; //dźwięk skoku
    public AudioClip landingSound; //dźwięk lądowania po skoku
    public AudioClip roarSound; //dźwięk ryku potwora

    private void Awake()
    {
        bossAudioSource = GameObject.Find("Boss1").GetComponent<AudioSource>(); //pobranie komponentu audio source
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        F3DAudio.PlayOneShotRandom(bossAudioSource, jumpSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dźwięku skoku
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
        roarTimer = roarInterval; // Inicjalizacja timera ryku potwora
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else {
            timer -= Time.deltaTime;
        }

        if(roarSound != null)
        {
            //obsługa ryczenia co 10 sekund
            if (roarTimer <= 0)
            {
                F3DAudio.PlayOneShotRandom(bossAudioSource, roarSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dźwięku ryku potwora
                roarTimer = roarInterval; // Resetowanie timera ryku potwora
            }
            else
            {
                roarTimer -= Time.deltaTime;
            }
        }

        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        F3DAudio.PlayOneShotRandom(bossAudioSource, landingSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dźwięku lądowania
    }

}

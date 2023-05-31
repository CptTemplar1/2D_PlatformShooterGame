using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introBehavior : StateMachineBehaviour {

    private int rand;

    private AudioSource bossAudioSource; //źródło odtwarzania dźwięku bossa
    public AudioClip roarSound; //dźwięk ryku potwora

    private void Awake()
    {
        bossAudioSource = GameObject.Find("Boss1").GetComponent<AudioSource>(); //pobranie komponentu audio source
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        F3DAudio.PlayOneShotRandom(bossAudioSource, roarSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dźwięku ryku potwora

        rand = Random.Range(0, 2);

        if (rand == 0)
        {
            animator.SetTrigger("idle");
        }
        else {
            animator.SetTrigger("jump");
        }
	}
    
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
    }

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}

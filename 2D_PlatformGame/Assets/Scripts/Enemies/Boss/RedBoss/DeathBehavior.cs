using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehavior : StateMachineBehaviour {

    private RedBossController boss;

    private AudioSource bossAudioSource; //źródło odtwarzania dźwięku bossa
    public AudioClip deathSound; //dźwięk śmierci potwora

    private void Awake()
    {
        bossAudioSource = GameObject.Find("Boss1").GetComponent<AudioSource>(); //pobranie komponentu audio source
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        F3DAudio.PlayOneShotRandom(bossAudioSource, deathSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dźwięku śmierci potwora
        boss = animator.GetComponent<RedBossController>();
        boss.isDead = true;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}


}

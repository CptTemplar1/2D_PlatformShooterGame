using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedBossController : Boss
{
    private Animator anim;
    private CoinsHandler coinsHandler;
    private TMP_Text coinAmount;
    bool onceDeath = false;

    protected override void Awake()
    {
        base.Awake();  //wykonanie metody Awake z klasy bazowej Boss
        anim = GetComponent<Animator>();
        coinAmount = GameObject.Find("coinAmount").GetComponent<TMP_Text>();
        coinsHandler = GameObject.Find("CollectedMoney").GetComponent<CoinsHandler>();
    }

    protected override void Update()
    {
        //instrukcje specyficzne dla nowej klasy dziedzicz�cej
        if (healthStatus.health <= (float)healthStatus.maxHealth / 3)
        {
            anim.SetTrigger("stageTwo");
        }

        if (healthStatus.health <= 0)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("idle");
            anim.SetTrigger("idle");
            anim.SetTrigger("death");
            if(onceDeath == false)
            {
                StaticCoins.addBossCoins(100);
                int coins = StaticCoins.get();
                coinsHandler.coins = coins;
                coinAmount.text = coins.ToString();
                onceDeath = true;
            }
        }
        else
        {
            //wykonanie metody Update z klasy bazowej Boss
            base.Update();
        }
    }
}

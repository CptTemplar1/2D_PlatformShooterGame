using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBossController : Boss
{
    private Animator anim;
    
    protected override void Awake()
    {
        base.Awake();  //wykonanie metody Awake z klasy bazowej Boss
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        //wykonanie metody Update z klasy bazowej Boss
        base.Update();

        //instrukcje specyficzne dla nowej klasy dziedzicz¹cej
        if (healthStatus.health <= (float)healthStatus.maxHealth / 3)
        {
            anim.SetTrigger("stageTwo");
        }

        if (healthStatus.health <= 0)
        {
            anim.SetTrigger("death");
        }
    }
}

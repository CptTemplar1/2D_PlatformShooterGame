using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthStatus : MonoBehaviour
{
    public int maxHealth; //maksymalna wartoœæ ¿ycia

    [HideInInspector]
    public int health; //aktualny stan zdrowia
    // Start is called before the first frame update
    void Awake()
    {
        if(maxHealth > 0)
        {
            health = maxHealth;
        }
        else
        {
            health = 1000;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

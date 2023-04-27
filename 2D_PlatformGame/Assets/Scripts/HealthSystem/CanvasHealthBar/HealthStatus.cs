using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatus : MonoBehaviour
{
    public int maxHealth; //maksymalna wartoœæ ¿ycia
    public int activeArmor; //aktualnie za³o¿ona zbroja

    [HideInInspector]
    public int health; //aktualny stan zdrowia

    private void Awake()
    {
        activeArmor = StaticWepaonSkin.currentArmor;

        if (activeArmor == 0)
        {
            maxHealth = 100;
            health = maxHealth;
        }
        else if (activeArmor == 1)
        {
            maxHealth = 150;
            health = maxHealth;
        }
        else if (activeArmor == 2)
        {
            maxHealth = 250;
            health = maxHealth;
        }
        else if (activeArmor == 3)
        {
            maxHealth = 450;
            health = maxHealth;
        }
        else if (activeArmor == 4)
        {
            maxHealth = 700;
            health = maxHealth;
        }
        else if (activeArmor == 5)
        {
            maxHealth = 1000;
            health = maxHealth;
        }
        else
            health = maxHealth;
    }
}

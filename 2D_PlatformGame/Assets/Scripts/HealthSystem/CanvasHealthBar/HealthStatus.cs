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
            maxHealth = StaticWepaonSkin.ownedArmorHp[0];
            health = maxHealth;
        }
        else if (activeArmor == 1)
        {
            maxHealth = StaticWepaonSkin.ownedArmorHp[1];
            health = maxHealth;
        }
        else if (activeArmor == 2)
        {
            maxHealth = StaticWepaonSkin.ownedArmorHp[2];
            health = maxHealth;
        }
        else if (activeArmor == 3)
        {
            maxHealth = StaticWepaonSkin.ownedArmorHp[3];
            health = maxHealth;
        }
        else if (activeArmor == 4)
        {
            maxHealth = StaticWepaonSkin.ownedArmorHp[4];
            health = maxHealth;
        }
        else if (activeArmor == 5)
        {
            maxHealth = StaticWepaonSkin.ownedArmorHp[5];
            health = maxHealth;
        }
        else
            health = maxHealth;
    }
}

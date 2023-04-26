using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatus : MonoBehaviour
{
    public int maxHealth; //maksymalna wartoœæ ¿ycia

    [HideInInspector]
    public int health; //aktualny stan zdrowia

    private void Awake()
    {
        health = maxHealth;
    }
}

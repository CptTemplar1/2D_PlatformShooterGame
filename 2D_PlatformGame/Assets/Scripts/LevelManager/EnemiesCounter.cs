using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    private TMP_Text enemiesAmount; 
    public int enemies = 0;

    void Awake()
    {
        enemiesAmount = GameObject.Find("enemiesAmount").GetComponent<TMP_Text>();

        enemiesAmount.text = enemies.ToString();
    }

    public void setEnemiesCounter(int amount)
    {
        enemies = amount;
        enemiesAmount.text = enemies.ToString();
    }

    public void decreaseEnemiesCounter()
    {
        if (enemies > 0)
        {
            enemies -= 1;

            enemiesAmount.text = enemies.ToString();
        }
    }
}

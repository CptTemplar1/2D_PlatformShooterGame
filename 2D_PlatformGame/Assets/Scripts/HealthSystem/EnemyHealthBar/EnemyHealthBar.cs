using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    //obiekt komponentu Enemy, gdzie znajduje się aktualne zdrowie przeciwnika
    private Enemy enemy;

    //zainicjalizowanie paska zdrowia aktualnym stanem zdrowia (na początku max)
    //oraz określenie zachowania podczas wystąpienia Eventu
    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemy.OnHealthChanged += OnHealthChanged;
        UpdateHealthBar();
    }

    private void OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }
    //aktualizuje stan paska zdrowia
    private void UpdateHealthBar()
    {
        transform.Find("Bar").localScale = new Vector3(enemy.GetHealthPercent(), 1);
    }

}
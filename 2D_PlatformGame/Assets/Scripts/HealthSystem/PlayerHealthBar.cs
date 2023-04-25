using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    //obiekt komponentu Enemy, gdzie znajduje się aktualne zdrowie przeciwnika
    private F3DCharacter character;

    //zainicjalizowanie paska zdrowia aktualnym stanem zdrowia (na początku max)
    //oraz określenie zachowania podczas wystąpienia Eventu
    private void Start()
    {
        //F3DCharacter character = collision.GetComponent<F3DCharacter>();
        //character = GetComponentInParent<Enemy>();
        //enemy.OnHealthChanged += OnHealthChanged;
        //UpdateHealthBar();
    }

    private void OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }
    //aktualizuje stan paska zdrowia
    private void UpdateHealthBar()
    {
        //transform.Find("Bar").localScale = new Vector3(enemy.GetHealthPercent(), 1);
    }

}
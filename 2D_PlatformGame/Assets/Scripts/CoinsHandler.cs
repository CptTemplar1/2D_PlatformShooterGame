using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsHandler : MonoBehaviour
{
    private TMP_Text coinAmount; //zmienna przechowuj�ca referencj� do licznika zebranych coin�w w Hud
    // Start is called before the first frame update
    void Awake()
    {
        coinAmount = GameObject.Find("coinAmount").GetComponent<TMP_Text>();
        coinAmount.text = StaticCoins.get().ToString();
    }

    //metoda dodaje coiny do salda gracza
    public void addCoin()
    {
        // Znajd� obiekt z komponentem wy�wietlaj�cym liczb� monet
        if (coinAmount != null)
        {
            // Zwi�ksz warto�� liczby monet i zaktualizuj wy�wietlany tekst
            StaticCoins.add();

            coinAmount.text = StaticCoins.get().ToString();
        }
    }
}

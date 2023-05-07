using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsHandler : MonoBehaviour
{
    private TMP_Text coinAmount; //zmienna przechowuj�ca referencj� do licznika zebranych coin�w w Hud
    public int coins = 0;
    // Start is called before the first frame update
    void Awake()
    {
        coins = StaticCoins.get();
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
            coins++;

            coinAmount.text = coins.ToString();
        }
    }
    //metoda oswiezajaca ilosc coinow w handlerze
    public void refresh()
    {
        coinAmount.text = StaticCoins.get().ToString();
    }
}

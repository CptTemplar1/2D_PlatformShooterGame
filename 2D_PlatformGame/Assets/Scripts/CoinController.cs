using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private TMP_Text coinAmount; //zmienna przechowuj�ca referencj� do licznika zebranych coin�w w Hud

    private bool isCollected = false; //zmienna okre�laj�ca czy dany pieni��ek zosta� ju� zebrany (aby uniemo�liwi� wielokrotn� obs�ug� kolizji przed znikni�ciem coina)

    private void Awake()
    {
        coinAmount = GameObject.Find("coinAmount").GetComponent<TMP_Text>();
        isCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // ustawiamy zmienn� na true, aby nie wykona� obs�ugi kolizji ponownie
            Destroy(gameObject); //usu� obiekt zebranego pieni��ka

            // Znajd� obiekt z komponentem wy�wietlaj�cym liczb� monet
            if (coinAmount != null)
            {
                // Zwi�ksz warto�� liczby monet i zaktualizuj wy�wietlany tekst
                int currentCoinAmount = int.Parse(coinAmount.text);
                int newCoinAmount = currentCoinAmount + 1;
                coinAmount.text = newCoinAmount.ToString();
            }
        }
        else if (collision.CompareTag("Ground"))
        {
            // Je�li kolizja z ziemi�, to dodaj impuls w g�r�, aby monetka podskoczy�a
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private TMP_Text coinAmount; //zmienna przechowuj¹ca referencjê do licznika zebranych coinów w Hud

    private bool isCollected = false; //zmienna okreœlaj¹ca czy dany pieni¹¿ek zosta³ ju¿ zebrany (aby uniemo¿liwiæ wielokrotn¹ obs³ugê kolizji przed znikniêciem coina)

    private void Awake()
    {
        coinAmount = GameObject.Find("coinAmount").GetComponent<TMP_Text>();
        isCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // ustawiamy zmienn¹ na true, aby nie wykonaæ obs³ugi kolizji ponownie
            Destroy(gameObject); //usuñ obiekt zebranego pieni¹¿ka

            // ZnajdŸ obiekt z komponentem wyœwietlaj¹cym liczbê monet
            if (coinAmount != null)
            {
                // Zwiêksz wartoœæ liczby monet i zaktualizuj wyœwietlany tekst
                int currentCoinAmount = int.Parse(coinAmount.text);
                int newCoinAmount = currentCoinAmount + 1;
                coinAmount.text = newCoinAmount.ToString();
            }
        }
        else if (collision.CompareTag("Ground"))
        {
            // Jeœli kolizja z ziemi¹, to dodaj impuls w górê, aby monetka podskoczy³a
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);
        }
    }
}

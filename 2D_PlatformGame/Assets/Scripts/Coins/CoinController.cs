using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private bool isCollected = false; //zmienna okre�laj�ca czy dany pieni��ek zosta� ju� zebrany (aby uniemo�liwi� wielokrotn� obs�ug� kolizji przed znikni�ciem coina)
    private CoinsHandler coinsHandler;
    private void Awake()
    {
        isCollected = false;
        coinsHandler = FindAnyObjectByType<CoinsHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            GetComponent<Rigidbody2D>().Sleep();
            isCollected = true; // ustawiamy zmienn� na true, aby nie wykona� obs�ugi kolizji ponownie
            coinsHandler.addCoin();
            Destroy(gameObject); //usu� obiekt zebranego pieni��ka
        }
        else if (collision.CompareTag("Ground") && collision.IsTouchingLayers(3))
        {
            // Je�li kolizja z ziemi�, to dodaj impuls w g�r�, aby monetka podskoczy�a
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);
        }
    }
}

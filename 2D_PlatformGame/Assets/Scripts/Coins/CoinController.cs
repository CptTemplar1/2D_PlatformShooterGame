using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private bool isCollected = false; //zmienna okreœlaj¹ca czy dany pieni¹¿ek zosta³ ju¿ zebrany (aby uniemo¿liwiæ wielokrotn¹ obs³ugê kolizji przed znikniêciem coina)
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
            isCollected = true; // ustawiamy zmienn¹ na true, aby nie wykonaæ obs³ugi kolizji ponownie
            coinsHandler.addCoin();
            Destroy(gameObject); //usuñ obiekt zebranego pieni¹¿ka
        }
        else if (collision.CompareTag("Ground") && collision.IsTouchingLayers(3))
        {
            // Jeœli kolizja z ziemi¹, to dodaj impuls w górê, aby monetka podskoczy³a
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);
        }
    }
}

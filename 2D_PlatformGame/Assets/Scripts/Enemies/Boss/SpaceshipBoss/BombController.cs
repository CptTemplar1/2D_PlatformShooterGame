using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    //TODO: Popraw prostowanie si� bomby w pozycji pionowej (o� Z), bo p�ki co jest lekko po skosie

    //w przypadku kolizjii zespawnuj wybuch oraz zniknij obiekt bomby
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //brak obs�ugi kolizji z obiektem z tagiem boss (aby bomby nie rozbija�y si� o statek)
        if(!collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject); //usuni�cie obiektu bomby ze sceny
            Debug.Log("BUM BUM BUM Bomba wybuch�a XD");
        }
    }
}

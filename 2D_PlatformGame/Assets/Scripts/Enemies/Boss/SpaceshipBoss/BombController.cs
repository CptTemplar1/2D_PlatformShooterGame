using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    //TODO: Popraw prostowanie siê bomby w pozycji pionowej (oœ Z), bo póki co jest lekko po skosie

    //w przypadku kolizjii zespawnuj wybuch oraz zniknij obiekt bomby
    private void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(gameObject); //usuniêcie obiektu bomby ze sceny
            Debug.Log("BUM BUM BUM Bomba wybuch³a XD");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    //TODO: Popraw prostowanie siê bomby w pozycji pionowej (oœ Z), bo póki co jest lekko po skosie

    public GameObject explosion; //prefab wybuchu bomby

    // W przypadku kolizji zespawnuj wybuch oraz zniknij obiekt bomby
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 collisionPosition = collision.GetContact(0).point; // Pobranie pozycji zderzenia
        Destroy(gameObject); // Usuniêcie obiektu bomby ze sceny
        GameObject bombExplosion = Instantiate(explosion, collisionPosition, Quaternion.identity); // Spawnowanie wybuchu na pozycji zderzenia
        Destroy(bombExplosion, 5f); //usuwanie obiektu wybuchu po up³ywie 5 sekund
    }
}

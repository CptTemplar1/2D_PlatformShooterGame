using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private SpaceshipBossController controller; //g³ówny skrypt obs³uguj¹cy zachowanie bossa statku kosmicznego
    private void Awake()
    {
        controller = GetComponentInParent<SpaceshipBossController>();
    }

    //kolizja gracza z polem do zrzutu bomby
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller.DropBomb();
        }
    }
}

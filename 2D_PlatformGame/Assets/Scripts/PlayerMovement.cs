using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f; // pr�dko�� gracza

    float horizontalMove = 0f; // kierunek zwrotu gracza
    bool jump = false; // zmienna u�ywana w metodzie Move, aby wykona� skok
    bool crouch = false; // zmienna u�ywana w metodzie Move, aby wykona� kucni�cie


    // Update is called once per frame
    void Update()
    {
        // strza�ka w lewo lub A zwraca -1
        // strza�ka w prawo lub D zwraza 1
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        // wy��czenie kucania po puszczeniu  przycisku
        else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
        }
    }

    // dzia�a tak samo jak Update, ale wykonuje si� sta�� ilo�� razy na sekund�, a nie raz na klatk�
    private void FixedUpdate()
    {
        // Player movement
        // Time.fixedDeltaTime zapewnia, �e pr�dko�� gracza b�dzie taka sama niezale�nie od tego ile razy wykona si� metoda
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        //skok si� wykona tylko raz po klikni�ciu przycisku (nawet jak b�dziemy go trzyma� wci�ni�tego)
        jump = false;
    }
}

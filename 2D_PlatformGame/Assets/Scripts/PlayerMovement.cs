using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f; // prêdkoœæ gracza

    float horizontalMove = 0f; // kierunek zwrotu gracza
    bool jump = false; // zmienna u¿ywana w metodzie Move, aby wykonaæ skok
    bool crouch = false; // zmienna u¿ywana w metodzie Move, aby wykonaæ kucniêcie


    // Update is called once per frame
    void Update()
    {
        // strza³ka w lewo lub A zwraca -1
        // strza³ka w prawo lub D zwraza 1
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        // wy³¹czenie kucania po puszczeniu  przycisku
        else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
        }
    }

    // dzia³a tak samo jak Update, ale wykonuje siê sta³¹ iloœæ razy na sekundê, a nie raz na klatkê
    private void FixedUpdate()
    {
        // Player movement
        // Time.fixedDeltaTime zapewnia, ¿e prêdkoœæ gracza bêdzie taka sama niezale¿nie od tego ile razy wykona siê metoda
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        //skok siê wykona tylko raz po klikniêciu przycisku (nawet jak bêdziemy go trzymaæ wciœniêtego)
        jump = false;
    }
}

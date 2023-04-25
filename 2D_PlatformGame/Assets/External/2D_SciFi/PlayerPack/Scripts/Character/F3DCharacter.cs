using UnityEngine;
using System.Collections;

public class F3DCharacter : MonoBehaviour
{
    public int Health;
    private F3DCharacterController _controller;
    private int _hitTriggerCounter;
    private float _hitTriggerTimer;
    private Rigidbody2D _rBody;
    private F3DWeaponController _weaponController;
    private bool _isDead;

    // Use this for initialization
    void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<F3DCharacterController>();
        _weaponController = GetComponent<F3DWeaponController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Debug.Log(Health);
    }

    public void OnDamage(int damageAmount)
    {
        if (_controller == null) 
            return;
        if (_isDead) 
            return;



        //Zadaj obrażenia i odrzuć gracza do tyłu
        if (Health > 0)
        {
            //Odrzuć gracza do tyłu po otrzymaniu obrażeń
            Vector2 knockbackForce = new Vector2(-1f, 1f) * 300000f;
            _rBody.AddForce(knockbackForce);

            Health -= damageAmount;
        }

        //obsłuż sytuację, gdy życie spadnie poniżej zera
        if (Health <= 0)
        {
            Health = 0;
            _isDead = true;

            //Sekwencja śmierci gracza
            _controller.Character.SetBool(Random.Range(-1f, 1f) > 0 ? "DeathFront" : "DeathBack", true);

            //Wyłączenie sterowania dla gracza po śmierci
            _controller.enabled = false;
            //zmiana warstwy martwego ciała z Player na Dead
            //gameObject.layer = LayerMask.NameToLayer("Dead");
            _rBody.drag = 2f;

//            for (int i = 0; i < _colliders.Length; i++)
//                _colliders[i].enabled = false;
            _weaponController.Drop();

            // Disable blob shadow under the character
            if (_controller.Shadow)
                _controller.Shadow.enabled = false;

            //
            return;
        }

        // Play Hit Animation and limit hit animation triggering 
        if (_hitTriggerCounter < 1)
            _controller.Character.SetTrigger("Hit");
        _hitTriggerCounter++;
    }

    private void LateUpdate()
    {
        // Dead... Quit trying
        if (_isDead) return;
    //    if (Input.GetKeyDown(KeyCode.K))
    //        OnDamage(1000);

        // Handle Hit Trigger timer
        if (_hitTriggerTimer > 0.5f) // <- Hit timer
        {
            _hitTriggerCounter = 0;
            _hitTriggerTimer = 0;
        }
        _hitTriggerTimer += Time.deltaTime;
    }
}
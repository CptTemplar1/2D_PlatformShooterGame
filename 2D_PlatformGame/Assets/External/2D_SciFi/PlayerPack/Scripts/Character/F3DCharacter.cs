using UnityEngine;
using System.Collections;

public class F3DCharacter : MonoBehaviour
{
    private F3DCharacterController _controller;
    private int _hitTriggerCounter;
    private float _hitTriggerTimer;
    private Rigidbody2D _rBody;
    private F3DWeaponController _weaponController;
    private bool _isDead;

    private HealthStatus healthStatus; // referencja do komponentu życia

    // Use this for initialization
    void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<F3DCharacterController>();
        _weaponController = GetComponent<F3DWeaponController>();

        healthStatus = GetComponent<HealthStatus>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Debug.Log(healthStatus.health);
    }

    public void OnDamage(int damageAmount, bool range)
    {
        if (_controller == null) 
            return;
        if (_isDead) 
            return;



        //Zadaj obrażenia i odrzuć gracza do tyłu
        if (healthStatus.health > 0)
        {
            Vector2 knockbackForce;
            //Odrzuć gracza do tyłu po otrzymaniu obrażeń
            if (range == false)
                knockbackForce = new Vector2(-1f, 1f) * 100000f;
            else
                knockbackForce = new Vector2(-1f, 1f) * 1000f;
            _rBody.AddForce(knockbackForce);

            healthStatus.health -= damageAmount;
        }

        //obsłuż sytuację, gdy życie spadnie poniżej zera
        if (healthStatus.health <= 0)
        {
            healthStatus.health = 0;
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
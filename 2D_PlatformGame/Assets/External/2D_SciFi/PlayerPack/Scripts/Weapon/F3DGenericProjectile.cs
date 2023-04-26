using System;
using UnityEngine;
using System.Collections;

public class F3DGenericProjectile : MonoBehaviour
{
    private Rigidbody2D _rBody; //rigidBody pocisku
    private Collider2D _collider; //collider pocisku
    private ParticleSystem _pSystem; //partickleSystem pocisku
    public AudioSource Audio;
    public F3DWeaponAudio.WeaponAudioInfo AudioInfo { get; set; }
    public F3DWeaponController.WeaponType WeaponType;

    protected Vector3 _origin; //miejsce wystrzału pocisku (koniec lufy)

    public Transform Hit;

    public bool PostHitHide; //schowanie pocisku po trafieniu
    public float DelayDespawn;
    public float HitLifeTime;

    public GameObject impactEffect; //efekt trafienia pocisku

    public virtual void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _pSystem = GetComponent<ParticleSystem>();
       
        _origin = transform.position;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Pobranie kolizji pocisku z przeciwnikiem
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(10);

        //Pobranie kolizji pocisku z bossem
        Boss boss = collision.gameObject.GetComponent<Boss>();
        if (boss != null)
            boss.TakeDamage(10);

        //DealDamage(5, WeaponType, contact.collider.transform, Hit, HitLifeTime, contact.point, contact.normal);

        // Play hit sound
        F3DWeaponAudio.OnProjectileImpact(Audio, AudioInfo);

        // Disable Physics if we have any
        if (_rBody != null && _collider != null)
        {
            _rBody.isKinematic = true;
            _collider.enabled = false;

            _rBody.simulated = false;
        }

        // Hide the sprite 
        if (PostHitHide)
        {
            _pSystem.Stop(true);
            _pSystem.Clear(true);
        }

        // spawnowanie efektu kolizji pocisku
        Instantiate(impactEffect, transform.position, transform.rotation);

        F3DSpawner.Despawn(transform, DelayDespawn);
    }

    public static bool DealDamage(int damageAmount, F3DWeaponController.WeaponType weaponType, Transform target, Transform hitPrefab, float hitLifeTime, Vector2 hitPoint, Vector2 hitNormal)
    {
        // Querry for F3DDamage
        if (target == null) return false;
        var damage = target.GetComponentInParent<F3DDamage>();
        if (damage == null) return false;

        //
        switch (damage.Type)
        {
            case F3DDamage.DamageType.Character:
                damage.OnDamage(damageAmount, hitPoint, hitNormal);
                switch (weaponType)
                {
                    case F3DWeaponController.WeaponType.Pistol:
                        break;
                    case F3DWeaponController.WeaponType.Assault:
                        break;
                    case F3DWeaponController.WeaponType.Shotgun:
                        break;
                    case F3DWeaponController.WeaponType.Machinegun:
                        break;
                    case F3DWeaponController.WeaponType.Sniper:
                        break;
                    case F3DWeaponController.WeaponType.Beam:
                        break;
                    case F3DWeaponController.WeaponType.Launcher:
                        break;
                    case F3DWeaponController.WeaponType.EnergyHeavy:
                        break;
                    case F3DWeaponController.WeaponType .Flamethrower:
                        break;
                    case F3DWeaponController.WeaponType.Tesla:
                        break;
                    case F3DWeaponController.WeaponType.Thrown:
                        break;
                    case F3DWeaponController.WeaponType.Knife:
                        break;
                    case F3DWeaponController.WeaponType.Melee:
                        break;
                    default:
                        break;
                }
                break;
            default:
                damage.OnDamage(damageAmount, hitPoint, hitNormal);
                break;
        }
        return true;
    }
}
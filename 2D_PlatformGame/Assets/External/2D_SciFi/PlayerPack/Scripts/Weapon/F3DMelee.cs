using UnityEngine;
using System.Collections;

public class F3DMelee : F3DGenericWeapon
{
    public F3DMeleeTrigger MeleeTrigger;
    private Collider2D _meleeTriggerCollider2D;
    public ParticleSystem MeleeTrail;

    public AudioSource HitAudio;

    public override void Awake()
    {
        base.Awake();
        if (MeleeTrigger != null)
        {
            // Disable trigger collider util the animation is triggered
            _meleeTriggerCollider2D = MeleeTrigger.GetComponent<Collider2D>();
            _meleeTriggerCollider2D.enabled = false;
        }
    }

    private int _stateSlashId = UnityEngine.Animator.StringToHash("Slash");
    private int _stateStabId = UnityEngine.Animator.StringToHash("Stab");

    public override void Fire(bool isSlash)
    {
        // Need MeleeTrigger Component for Melee Weapons
        if (MeleeTrigger == null) return;

        // Check before firing
        if (!Animator.isInitialized) return;
        if (_fireTimer < FireRate) return;

        // Set Fire Timer vars
        _fireTimer = 0;

        // Manually Play the animation to avoid random desync with the melee trail
        Animator.Play(isSlash ? _stateSlashId : _stateStabId);

        // Activate Melee Trail
        if (MeleeTrail && isSlash)
        {
            MeleeTrail.Simulate(0f, false);
            MeleeTrail.Clear();
            MeleeTrail.Play();
        }

        // Toggle Trigger Collider for a short period of time uppon stab/slash animation
        _meleeTriggerCollider2D.enabled = true;
        StartCoroutine(DisableMeleeTrigger(FireRate));
    }

    public override void Stop() { }

    private IEnumerator DisableMeleeTrigger(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);

        // Disable
        _meleeTriggerCollider2D.enabled = false;
    }

    public void OnMeleeHit(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(20);

        //F3DGenericProjectile.DealDamage(5, Type, collision.transform, BarrelSpark, 1f,
        //collision.bounds.ClosestPoint(FXSocket.position), FXSocket.up);
    }
}
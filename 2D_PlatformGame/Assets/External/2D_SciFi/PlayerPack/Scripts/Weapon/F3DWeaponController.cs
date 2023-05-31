using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class F3DWeaponController : MonoBehaviour
{
    public AudioSource knifeSoundSource; //źródło dźwięku ataku nożem
    public AudioClip knifeSlashSound; //dźwięk cięcia nożem
    public AudioClip knifeStabSound; //dźwięk dźgnięcia nożem
    
    public int EquippedSlot;
    public int EquippedWeapon;
    public List<WeaponSlot> Slots;
    static public Dictionary<WeaponType, bool> ownedWeapon = new Dictionary<WeaponType, bool>();
    //
    private F3DCharacterAvatar _avatar;

    //
    [Serializable]
    public class WeaponSlot
    {
        [SerializeField] public List<F3DGenericWeapon> Weapons;
        public int WeaponSlotCounter = 1;

        public void Forward()
        {
            WeaponSlotCounter++;
            if (WeaponSlotCounter >= Weapons.Count)
                WeaponSlotCounter = 0;
        }
    }

    public enum WeaponType
    {
        Pistol,
        PistolLaser,
        PistolPlasma,
        Assault,
        AssaultPlasma,
        Shotgun,
        Sniper,
        Machinegun,
        Knife,
        Melee
    }

    private void Awake()
    {
        ownedWeapon = StaticWepaonSkin.getWeapon();
        _avatar = GetComponent<F3DCharacterAvatar>();

        ActivateWeapon(EquippedSlot, EquippedWeapon);
    }

    // Use this for initialization
    private void Start() { }


    // Update is called once per frame
    private void Update()
    {
        //Obsluga wystrzału/ataku
        //w przypadku kliknięcia LPM lub PPM sprawdzamy czy gracz nie ma wyekwipowanego noża (6, 0), aby wtedy wywołać metodę z parametrem bool
        if (Input.GetMouseButtonDown(0))
        {
            if (EquippedSlot == 8 && EquippedWeapon == 0)
            {
                Slots[EquippedSlot].Weapons[EquippedWeapon].Fire(false);
                F3DAudio.PlayOneShotRandom(knifeSoundSource, knifeStabSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dźwięku ataku nożem
            }
            else
                Slots[EquippedSlot].Weapons[EquippedWeapon].Fire();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (EquippedSlot == 8 && EquippedWeapon == 0)
            {
                Slots[EquippedSlot].Weapons[EquippedWeapon].Fire(true);
                F3DAudio.PlayOneShotRandom(knifeSoundSource, knifeSlashSound, new Vector2(0.9f, 1f), new Vector2(0.9f, 1f)); //odtworzenie dźwięku ataku nożem
            }
        }

        // Stop
        if (Input.GetMouseButtonUp(0))
        {
            Slots[EquippedSlot].Weapons[EquippedWeapon].Stop();
        }
      
        // Switch Weapon Slot
        if (Input.GetKeyDown(KeyCode.Alpha1) && ownedWeapon[WeaponType.Pistol] == true)
            ActivateSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha8) && ownedWeapon[WeaponType.PistolLaser] == true)
            ActivateSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha2) && ownedWeapon[WeaponType.PistolPlasma] == true)
            ActivateSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha3) && ownedWeapon[WeaponType.Assault] == true)
            ActivateSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha4) && ownedWeapon[WeaponType.AssaultPlasma] == true)
            ActivateSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha5) && ownedWeapon[WeaponType.Shotgun] == true)
            ActivateSlot(5);    
        if (Input.GetKeyDown(KeyCode.Alpha6) && ownedWeapon[WeaponType.Sniper] == true)
            ActivateSlot(6);
        if (Input.GetKeyDown(KeyCode.Alpha7) && ownedWeapon[WeaponType.Machinegun] == true)
            ActivateSlot(7);
        if (Input.GetKeyDown(KeyCode.F) && ownedWeapon[WeaponType.Knife] == true)
            ActivateSlot(8);
    }

    private void ActivateSlot(int slot)
    {
        if (slot > Slots.Count - 1) return;
        if (slot == EquippedSlot)
            Slots[EquippedSlot].Forward();
        EquippedSlot = slot;
        EquippedWeapon = Slots[EquippedSlot].WeaponSlotCounter;
        ActivateWeapon(EquippedSlot, EquippedWeapon);
    }

    //////////////////////////////////////////////////////////////
    // Pass animation data to an active weapon controller
    public void SetBool(string var, bool value)
    {
        Slots[EquippedSlot].Weapons[EquippedWeapon].Animator.SetBool(var, value);
    }

    public void SetFloat(string var, float value)
    {
        Slots[EquippedSlot].Weapons[EquippedWeapon].Animator.SetFloat(var, value);
    }
    //////////////////////////////////////////////////////////////

    // Weapon activation
    private void ActivateWeapon(int slot, int weapon)
    {
        EquippedSlot = slot;
        EquippedWeapon = weapon;
        for (var i = 0; i < Slots.Count; i++)
        {
            for (var y = 0; y < Slots[i].Weapons.Count; y++)
                Slots[i].Weapons[y].gameObject.SetActive(slot == i && weapon == y);
        }
     //   Slots[slot].Weapons[weapon].OnAnimationReadyEvent();

        UpdateCharacterHands(_avatar.Characters[_avatar.CharacterId]);
    }

    

    // Weapon drop on character killed
    public void Drop()
    {
        var powerUpPrefab = Slots[EquippedSlot].Weapons[EquippedWeapon].PowerUp;
        if (powerUpPrefab != null)
        {
            // Random Rotation
            var rot = Slots[EquippedSlot].Weapons[EquippedWeapon].FXSocket.rotation *
                      Quaternion.Euler(0, 0, Random.Range(-5, 5));

            // Spawn
            var powerUp = F3DSpawner.Spawn(powerUpPrefab, Slots[EquippedSlot].Weapons[EquippedWeapon].FXSocket.position,
                rot, null);

            // Flip
            var powerUpFlip = Mathf.Sign(Slots[EquippedSlot].Weapons[EquippedWeapon].FXSocket.lossyScale.x);
            var curScale = powerUp.localScale;
            curScale.x *= powerUpFlip;
            powerUp.localScale = curScale;

            // Add random force / torque
            var powerUpRb = powerUp.GetComponent<Rigidbody2D>();
            powerUpRb.AddForce((Vector2) Slots[EquippedSlot].Weapons[EquippedWeapon].FXSocket.up * Random.Range(8, 12),
                ForceMode2D.Impulse);
            powerUpRb.AddTorque(Random.Range(-250, 250), ForceMode2D.Force);
        }

        // Deactivate components
        Slots[EquippedSlot].Weapons[EquippedWeapon].gameObject.SetActive(false);
        this.enabled = false;
    }

    public F3DGenericWeapon GetCurrentWeapon()
    {
        return Slots[EquippedSlot].Weapons[EquippedWeapon];
    }

    // Avatar Hands
    public void UpdateCharacterHands(F3DCharacterAvatar.CharacterArmature armature)
    {
        var myWeapon = GetCurrentWeapon();
        myWeapon.LeftHand.sprite = GetSpriteFromHandId(myWeapon.LeftHandId, armature);
        myWeapon.RightHand.sprite = GetSpriteFromHandId(myWeapon.RightHandId, armature);
    }



    private Sprite GetSpriteFromHandId(int id, F3DCharacterAvatar.CharacterArmature armature)
    {
        switch (id)
        {
            case 0:
                return armature.Hand1;
            case 1:
                return armature.Hand2;
            case 2:
                return armature.Hand3;
            case 3:
                return armature.Hand4;
            default:
                return armature.Hand1;
        }
    }
}
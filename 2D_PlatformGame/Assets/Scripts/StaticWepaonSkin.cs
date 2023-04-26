using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static F3DWeaponController;

public static class StaticWepaonSkin
{
    static public Dictionary<WeaponType, bool> ownedWeapon = new Dictionary<WeaponType, bool>();

    static public void reset()
    {
        ownedWeapon.Add(WeaponType.Pistol, true);
        ownedWeapon.Add(WeaponType.PistolLaser, false);
        ownedWeapon.Add(WeaponType.PistolPlasma, false);
        ownedWeapon.Add(WeaponType.Assault, false);
        ownedWeapon.Add(WeaponType.AssaultPlasma, false);
        ownedWeapon.Add(WeaponType.Shotgun, false);
        ownedWeapon.Add(WeaponType.Sniper, false);
        ownedWeapon.Add(WeaponType.Machinegun, false);
        ownedWeapon.Add(WeaponType.Knife, true);
        ownedWeapon.Add(WeaponType.Melee, true);
    }

    static public Dictionary<WeaponType, bool>  getWeapon()
    {
        return ownedWeapon;
    }
}

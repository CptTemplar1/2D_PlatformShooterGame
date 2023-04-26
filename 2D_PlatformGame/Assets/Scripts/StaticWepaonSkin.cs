using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static F3DWeaponController;

public static class StaticWepaonSkin
{
    static public Dictionary<WeaponType, bool> ownedWeapon = new Dictionary<WeaponType, bool>();
    static public Dictionary<int, bool> ownedArmor = new Dictionary<int, bool>();
    static public int currentArmor;
    static public void resetWeapon()
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
    
    static public void resetArmor()
    {
        ownedArmor.Add(0, true);
        ownedArmor.Add(1, false);
        ownedArmor.Add(2, false);
        ownedArmor.Add(3, false);
        ownedArmor.Add(4, false);
        ownedArmor.Add(5, false);
        currentArmor = 0;
    }

    static public Dictionary<WeaponType, bool>  getWeapon()
    {
        return ownedWeapon;
    }

    static public Dictionary<int, bool> getArmor()
    {
        return ownedArmor;
    }

    static public void setCurrentArmor(int armor)
    {
        if (armor >= 0 && armor < 6)
        {
            currentArmor = armor;
        }
        else
        {
            currentArmor = 0;
        }
    }
}

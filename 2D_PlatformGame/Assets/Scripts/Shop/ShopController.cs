using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public List<TMP_Text> texts = new List<TMP_Text>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Pistol] == true)
        {
            buttons[0].enabled = false;
            texts[0].text = "OWNED";
            texts[0].color = Color.green;
        }
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.PistolLaser] == true)
        {
            buttons[1].enabled = false;
            texts[1].text = "OWNED";
            texts[1].color = Color.green;
        }
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.PistolPlasma] == true)
        {
            buttons[2].enabled = false;
            texts[2].text = "OWNED";
            texts[2].color = Color.green;
        }
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Assault] == true)
        {
            buttons[3].enabled = false;
            texts[3].text = "OWNED";
            texts[3].color = Color.green;
        }
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.AssaultPlasma] == true)
        {
            buttons[4].enabled = false;
            texts[4].text = "OWNED";
            texts[4].color = Color.green;
        }
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Shotgun] == true)
        {
            buttons[5].enabled = false;
            texts[5].text = "OWNED";
            texts[5].color = Color.green;
        }
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Sniper] == true)
        {
            buttons[6].enabled = false;
            texts[6].text = "OWNED";
            texts[6].color = Color.green;
        }
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Machinegun] == true)
        {
            buttons[7].enabled = false;
            texts[7].text = "OWNED";
            texts[7].color = Color.green;
        }
        if (StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Knife] == true)
        {
            buttons[8].enabled = false;
            texts[8].text = "OWNED";
            texts[8].color = Color.green;
        }
    }

    public void buyPistol()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Pistol] = true;
    }

    public void buyPlasmaPistol()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.PistolPlasma] = true;
    }

    public void buyLaserPistol()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.PistolLaser] = true;
    }

    public void buyAssault()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Assault] = true;
    }

    public void buyAssaultPlasma()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.AssaultPlasma] = true;
    }

    public void buyShotgun()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Shotgun] = true;
    }

    public void buySniper()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Sniper] = true;
    }

    public void buyMachinegun()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Machinegun] = true;
    }

    public void buyKnife()
    {
        StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Knife] = true;
    }
}

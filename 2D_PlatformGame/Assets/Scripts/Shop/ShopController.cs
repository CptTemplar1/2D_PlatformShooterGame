using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public List<TMP_Text> texts = new List<TMP_Text>();

    public int KnifePrice = 0;
    public int PistolPrice = 0;
    public int PistolPlasmaPrice = 5;
    public int AssaultPrice = 20;
    public int AssaultPlasmaPrice = 50;
    public int ShotgunPrice = 100;
    public int SniperPrice = 250;
    public int MachinegunPrice = 800;
    public int PistolLaserPrice = 2000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setPrices()
    {
        texts[0].text = "BUY   " + PistolPrice;
        texts[1].text = "BUY   " + PistolLaserPrice;
        texts[2].text = "BUY   " + PistolPlasmaPrice;
        texts[3].text = "BUY   " + AssaultPrice;
        texts[4].text = "BUY   " + AssaultPlasmaPrice;
        texts[5].text = "BUY   " + ShotgunPrice;
        texts[6].text = "BUY   " + SniperPrice;
        texts[7].text = "BUY   " + MachinegunPrice;
        texts[8].text = "BUY   " + KnifePrice;
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
        if(StaticCoins.get() >= PistolPrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Pistol] = true;
            StaticCoins.minus(PistolPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyPlasmaPistol()
    {
        if (StaticCoins.get() >= PistolPlasmaPrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.PistolPlasma] = true;
            StaticCoins.minus(PistolPlasmaPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyLaserPistol()
    {
        if (StaticCoins.get() >= PistolLaserPrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.PistolLaser] = true;
            StaticCoins.minus(PistolLaserPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyAssault()
    {
        if (StaticCoins.get() >= AssaultPrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Assault] = true;
            StaticCoins.minus(AssaultPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyAssaultPlasma()
    {
        if (StaticCoins.get() >= AssaultPlasmaPrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.AssaultPlasma] = true;
            StaticCoins.minus(AssaultPlasmaPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyShotgun()
    {
        if (StaticCoins.get() >= ShotgunPrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Shotgun] = true;
            StaticCoins.minus(ShotgunPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buySniper()
    {
        if (StaticCoins.get() >= SniperPrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Sniper] = true;
            StaticCoins.minus(SniperPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyMachinegun()
    {
        if (StaticCoins.get() >= MachinegunPrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Machinegun] = true;
            StaticCoins.minus(MachinegunPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyKnife()
    {
        if (StaticCoins.get() >= KnifePrice)
        {
            StaticWepaonSkin.ownedWeapon[F3DWeaponController.WeaponType.Knife] = true;
            StaticCoins.minus(KnifePrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }
}

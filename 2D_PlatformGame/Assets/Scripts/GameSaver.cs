using UnityEngine;
using UnityEngine.SceneManagement;
using static F3DWeaponController;

public class GameSaver : MonoBehaviour
{
    void Awake()
    {
        //odczyt coinow
        StaticCoins.add(PlayerPrefs.GetInt("Coins", 0));
        if (SceneManager.GetActiveScene().name == "Menu")
            FindObjectOfType<CoinsHandler>().refresh();

        //odczyt czy to pierwsze wystartowanie gry
        NewGameStatic.isStarted = GetBool("isStarted");

        //odczyt posiadanych broni
        StaticWepaonSkin.ownedWeapon[WeaponType.Pistol] = GetBool("Pistol");
        StaticWepaonSkin.ownedWeapon[WeaponType.PistolLaser] = GetBool("PistolLaser");
        StaticWepaonSkin.ownedWeapon[WeaponType.PistolPlasma] = GetBool("PistolPlasma");
        StaticWepaonSkin.ownedWeapon[WeaponType.Assault] = GetBool("Assault");
        StaticWepaonSkin.ownedWeapon[WeaponType.AssaultPlasma] = GetBool("AssaultPlasma");
        StaticWepaonSkin.ownedWeapon[WeaponType.Shotgun] = GetBool("Shotgun");
        StaticWepaonSkin.ownedWeapon[WeaponType.Sniper] = GetBool("Sniper");
        StaticWepaonSkin.ownedWeapon[WeaponType.Machinegun] = GetBool("Machinegun");
        StaticWepaonSkin.ownedWeapon[WeaponType.Knife] = GetBool("Knife");
        StaticWepaonSkin.ownedWeapon[WeaponType.Melee] = GetBool("Melee");

        //odczyt posiadanych zbroi
        StaticWepaonSkin.ownedArmor[0] = GetBool("Basic");
        StaticWepaonSkin.ownedArmor[1] = GetBool("Knight");
        StaticWepaonSkin.ownedArmor[2] = GetBool("Toxic");
        StaticWepaonSkin.ownedArmor[3] = GetBool("Soldier");
        StaticWepaonSkin.ownedArmor[4] = GetBool("LuxoJr");
        StaticWepaonSkin.ownedArmor[5] = GetBool("Titan");

        //odczyt hp zbroi
        StaticWepaonSkin.ownedArmorHp[0] = PlayerPrefs.GetInt("BasicHP");
        StaticWepaonSkin.ownedArmorHp[1] = PlayerPrefs.GetInt("KnightHP");
        StaticWepaonSkin.ownedArmorHp[2] = PlayerPrefs.GetInt("ToxicHP");
        StaticWepaonSkin.ownedArmorHp[3] = PlayerPrefs.GetInt("SoldierHP");
        StaticWepaonSkin.ownedArmorHp[4] = PlayerPrefs.GetInt("LuxoJrHP");
        StaticWepaonSkin.ownedArmorHp[5] = PlayerPrefs.GetInt("TitanHP");

        //odczyt aktualnie za³o¿onej zbroi
        StaticWepaonSkin.setCurrentArmor(PlayerPrefs.GetInt("CurrentArmor"));

        //odczyt ukoñczonych leveli
        PassedLevels.passedLevels[1] = GetBool("Level1");
        PassedLevels.passedLevels[2] = GetBool("Level2");
        PassedLevels.passedLevels[3] = GetBool("Level3");
        PassedLevels.passedLevels[4] = GetBool("Level4");
        PassedLevels.passedLevels[5] = GetBool("Level5");
        PassedLevels.passedLevels[6] = GetBool("Level6");
        PassedLevels.passedLevels[7] = GetBool("Level7");
        PassedLevels.passedLevels[8] = GetBool("Level8");
        PassedLevels.passedLevels[9] = GetBool("Level9");

        //SetBool("isStarted", true);
    }

    //metoda zapisuje wszystkie wa¿ne statystyki oraz dane
    public void SaveStats()
    {
        //zapis coinow
        PlayerPrefs.SetInt("Coins", StaticCoins.get());

        //zapis czy to pierwsze wystartowanie gry
        SetBool("isStarted", NewGameStatic.isStarted);

        //zapis posiadanych broni gracza
        SetBool("Pistol", StaticWepaonSkin.ownedWeapon[WeaponType.Pistol]);
        SetBool("PistolLaser", StaticWepaonSkin.ownedWeapon[WeaponType.PistolLaser]);
        SetBool("PistolPlasma", StaticWepaonSkin.ownedWeapon[WeaponType.PistolPlasma]);
        SetBool("Assault", StaticWepaonSkin.ownedWeapon[WeaponType.Assault]);
        SetBool("AssaultPlasma", StaticWepaonSkin.ownedWeapon[WeaponType.AssaultPlasma]);
        SetBool("Shotgun", StaticWepaonSkin.ownedWeapon[WeaponType.Shotgun]);
        SetBool("Sniper", StaticWepaonSkin.ownedWeapon[WeaponType.Sniper]);
        SetBool("Machinegun", StaticWepaonSkin.ownedWeapon[WeaponType.Machinegun]);
        SetBool("Knife", StaticWepaonSkin.ownedWeapon[WeaponType.Knife]);
        SetBool("Melee", StaticWepaonSkin.ownedWeapon[WeaponType.Melee]);

        //zapis posiadanych zbroi gracza
        SetBool("Basic", StaticWepaonSkin.ownedArmor[0]);
        SetBool("Knight", StaticWepaonSkin.ownedArmor[1]);
        SetBool("Toxic", StaticWepaonSkin.ownedArmor[2]);
        SetBool("Soldier", StaticWepaonSkin.ownedArmor[3]);
        SetBool("LuxoJr", StaticWepaonSkin.ownedArmor[4]);
        SetBool("Titan", StaticWepaonSkin.ownedArmor[5]);

        //zapis hp ka¿dej zbroi
        PlayerPrefs.SetInt("BasicHP", StaticWepaonSkin.ownedArmorHp[0]);
        PlayerPrefs.SetInt("KnightHP", StaticWepaonSkin.ownedArmorHp[1]);
        PlayerPrefs.SetInt("ToxicHP", StaticWepaonSkin.ownedArmorHp[2]);
        PlayerPrefs.SetInt("SoldierHP", StaticWepaonSkin.ownedArmorHp[3]);
        PlayerPrefs.SetInt("LuxoJrHP", StaticWepaonSkin.ownedArmorHp[4]);
        PlayerPrefs.SetInt("TitanHP", StaticWepaonSkin.ownedArmorHp[5]);

        //zapis aktualnie za³o¿onej zbroi
        PlayerPrefs.SetInt("CurrentArmor", StaticWepaonSkin.currentArmor);

        //zapis lvli, które gracz ukoñczy³
        SetBool("Level1", PassedLevels.passedLevels[1]);
        SetBool("Level2", PassedLevels.passedLevels[2]);
        SetBool("Level3", PassedLevels.passedLevels[3]);
        SetBool("Level4", PassedLevels.passedLevels[4]);
        SetBool("Level5", PassedLevels.passedLevels[5]);
        SetBool("Level6", PassedLevels.passedLevels[6]);
        SetBool("Level7", PassedLevels.passedLevels[7]);
        SetBool("Level8", PassedLevels.passedLevels[8]);
        SetBool("Level9", PassedLevels.passedLevels[9]);
    }

    //Testowe dodawanie 1000 coinow
    public void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            StaticCoins.addBossCoins(1000);
            if (SceneManager.GetActiveScene().name == "Menu")
                FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    //zapisuje wartosci bool do PlayerPrefs
    private void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    //zwraca true, gdy wartosc jest rowna 1 lub false gdy wartosc rowna 0
    private bool GetBool(string key)
    {
        if (PlayerPrefs.GetInt(key) == 1)
            return true;
        else
            return false;
    }
}

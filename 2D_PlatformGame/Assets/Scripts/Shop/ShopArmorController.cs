using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopArmorController : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public List<TMP_Text> texts = new List<TMP_Text>();
    public List<Button> armorToEquip = new List<Button>();
    public List<TMP_Text> equippedTexts = new List<TMP_Text>();
    public List<TMP_Text> healthText = new List<TMP_Text>();

    public int BasicPrice = 0;
    public int KnightPrice = 10;
    public int ToxicPrice = 100;
    public int SoldierPrice = 500;
    public int LuxoJrPrice = 1250;
    public int TitanPrice = 3000;

    void Start()
    {
        healthText[0].text = StaticWepaonSkin.ownedArmorHp[0].ToString();
        healthText[1].text = StaticWepaonSkin.ownedArmorHp[1].ToString();
        healthText[2].text = StaticWepaonSkin.ownedArmorHp[2].ToString();
        healthText[3].text = StaticWepaonSkin.ownedArmorHp[3].ToString();
        healthText[4].text = StaticWepaonSkin.ownedArmorHp[4].ToString();
        healthText[5].text = StaticWepaonSkin.ownedArmorHp[5].ToString();
    }

    public void setPrices()
    {
        texts[0].text = "BUY   " + BasicPrice;
        texts[1].text = "BUY   " + KnightPrice;
        texts[2].text = "BUY   " + ToxicPrice;
        texts[3].text = "BUY   " + SoldierPrice;
        texts[4].text = "BUY   " + LuxoJrPrice;
        texts[5].text = "BUY   " + TitanPrice;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticWepaonSkin.ownedArmor[0] == true)
        {
            buttons[0].enabled = false;
            texts[0].text = "OWNED";
            texts[0].color = Color.green;
            armorToEquip[0].gameObject.SetActive(true);
        }
        if (StaticWepaonSkin.ownedArmor[1] == true)
        {
            buttons[1].enabled = false;
            texts[1].text = "OWNED";
            texts[1].color = Color.green;
            armorToEquip[1].gameObject.SetActive(true);
        }
        if (StaticWepaonSkin.ownedArmor[2] == true)
        {
            buttons[2].enabled = false;
            texts[2].text = "OWNED";
            texts[2].color = Color.green;
            armorToEquip[2].gameObject.SetActive(true);
        }
        if (StaticWepaonSkin.ownedArmor[3] == true)
        {
            buttons[3].enabled = false;
            texts[3].text = "OWNED";
            texts[3].color = Color.green;
            armorToEquip[3].gameObject.SetActive(true);
        }
        if (StaticWepaonSkin.ownedArmor[4] == true)
        {
            buttons[4].enabled = false;
            texts[4].text = "OWNED";
            texts[4].color = Color.green;
            armorToEquip[4].gameObject.SetActive(true);
        }
        if (StaticWepaonSkin.ownedArmor[5] == true)
        {
            buttons[5].enabled = false;
            texts[5].text = "OWNED";
            texts[5].color = Color.green;
            armorToEquip[5].gameObject.SetActive(true);
        }
        if (StaticWepaonSkin.currentArmor == 0)
        {
            equippedTexts[0].gameObject.SetActive(true);
        }
        else
            equippedTexts[0].gameObject.SetActive(false);
        if (StaticWepaonSkin.currentArmor == 1)
        {
            equippedTexts[1].gameObject.SetActive(true);
        }
        else
            equippedTexts[1].gameObject.SetActive(false);
        if (StaticWepaonSkin.currentArmor == 2)
        {
            equippedTexts[2].gameObject.SetActive(true);
        }
        else
            equippedTexts[2].gameObject.SetActive(false);
        if (StaticWepaonSkin.currentArmor == 3)
        {
            equippedTexts[3].gameObject.SetActive(true);
        }
        else
            equippedTexts[3].gameObject.SetActive(false);
        if (StaticWepaonSkin.currentArmor == 4)
        {
            equippedTexts[4].gameObject.SetActive(true);
        }
        else
            equippedTexts[4].gameObject.SetActive(false);
        if (StaticWepaonSkin.currentArmor == 5)
        {
            equippedTexts[5].gameObject.SetActive(true);
        }
        else
            equippedTexts[5].gameObject.SetActive(false);
    }

    public void buyBasic()
    {
        if (StaticCoins.get() >= BasicPrice)
        {
            StaticWepaonSkin.ownedArmor[0] = true;
            StaticCoins.minus(BasicPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyKnight()
    {
        if (StaticCoins.get() >= KnightPrice)
        {
            StaticWepaonSkin.ownedArmor[1] = true;
            StaticCoins.minus(KnightPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyToxic()
    {
        if (StaticCoins.get() >= ToxicPrice)
        {
            StaticWepaonSkin.ownedArmor[2] = true;
            StaticCoins.minus(ToxicPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buySoldier()
    {
        if (StaticCoins.get() >= SoldierPrice)
        {
            StaticWepaonSkin.ownedArmor[3] = true;
            StaticCoins.minus(SoldierPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyLuxoJr()
    {
        if (StaticCoins.get() >= LuxoJrPrice)
        {
            StaticWepaonSkin.ownedArmor[4] = true;
            StaticCoins.minus(LuxoJrPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void buyTitan()
    {
        if (StaticCoins.get() >= TitanPrice)
        {
            StaticWepaonSkin.ownedArmor[5] = true;
            StaticCoins.minus(TitanPrice);
            FindObjectOfType<CoinsHandler>().refresh();
        }
    }

    public void equipBasic()
    {
        StaticWepaonSkin.setCurrentArmor(0);
    }

    public void equipKnight()
    {
        StaticWepaonSkin.setCurrentArmor(1);
    }

    public void equipToxic()
    {
        StaticWepaonSkin.setCurrentArmor(2);
    }

    public void equipSoldier()
    {
        StaticWepaonSkin.setCurrentArmor(3);
    }

    public void equipLuxoJr()
    {
        StaticWepaonSkin.setCurrentArmor(4);
    }

    public void equipTitan()
    {
        StaticWepaonSkin.setCurrentArmor(5);
    }
}

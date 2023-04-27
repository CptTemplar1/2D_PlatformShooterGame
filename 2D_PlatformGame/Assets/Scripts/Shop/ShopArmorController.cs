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
        StaticWepaonSkin.ownedArmor[0] = true;
    }

    public void buyKnight()
    {
        StaticWepaonSkin.ownedArmor[1] = true;
    }

    public void buyToxic()
    {
        StaticWepaonSkin.ownedArmor[2] = true;
    }

    public void buySoldier()
    {
        StaticWepaonSkin.ownedArmor[3] = true;
    }

    public void buyLuxoJr()
    {
        StaticWepaonSkin.ownedArmor[4] = true;
    }

    public void buyTitan()
    {
        StaticWepaonSkin.ownedArmor[5] = true;
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

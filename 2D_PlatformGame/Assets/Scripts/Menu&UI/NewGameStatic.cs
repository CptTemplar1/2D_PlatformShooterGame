using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NewGameStatic
{
    public static bool isStarted = false;

    public static void setNewGame()
    {
        if (!isStarted)
        {
            StaticWepaonSkin.resetWeapon();
            StaticWepaonSkin.resetArmor();
            PassedLevels.resetPassedLevels();
        }
        isStarted = true;
    }
}

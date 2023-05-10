using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PassedLevels
{
    public static Dictionary<int, bool> passedLevels = new Dictionary<int, bool>();

    public static void resetPassedLevels()
    {
        passedLevels[1] = false;
        passedLevels[2] = false;
        passedLevels[3] = false;
        passedLevels[4] = false;
        passedLevels[5] = false;
        passedLevels[6] = false;
        passedLevels[7] = false;
        passedLevels[8] = false;
        passedLevels[9] = false;
    }

    public static void setPassedLevel(int level)
    {
        passedLevels[level] = true;
    }

}

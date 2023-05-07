using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticCoins
{
    static public int coins;

    static public void add()
    {
        coins++;
    }

    static public void add(int amount)
    {
        coins = amount;
    }

    static public int get()
    {
        return coins;
    }

    static public void minus(int amount)
    {
        coins -= amount;
    }
}

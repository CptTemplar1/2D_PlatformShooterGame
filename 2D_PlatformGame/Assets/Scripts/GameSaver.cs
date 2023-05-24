using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour
{

    void Awake()
    {
        StaticCoins.add(PlayerPrefs.GetInt("Coins", 0));
        if(SceneManager.GetActiveScene().name == "Menu")
            FindObjectOfType<CoinsHandler>().refresh();
    }

    public void SaveStats()
    {
        Debug.Log(StaticCoins.get());
        PlayerPrefs.SetInt("Coins", StaticCoins.get());
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            StaticCoins.addBossCoins(1000);
        }
    }
}

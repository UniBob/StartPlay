using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldKeeperScript : MonoBehaviour
{
    public delegate void GoldUpdate(int currentGold);
    public static GoldUpdate goldUpdate;

    private int goldAmount;

    public void IncreeseGoldAmount(int i)
    { 
        goldAmount = i; 
        if (goldUpdate!=null) goldUpdate(goldAmount);
    }
    public int GetGoldAmount()
    { 
        return goldAmount; 
    }
    void SaveGold()
    {
        PlayerPrefs.SetInt(PrefsKeys.currentGoldKey, goldAmount);
    }
    void Start()
    {
        Player.Save += SaveGold;

        if (PlayerPrefs.HasKey(PrefsKeys.currentGoldKey))
        {
            goldAmount = PlayerPrefs.GetInt(PrefsKeys.currentGoldKey);
        }
        else
        {
            goldAmount = 0;
            PlayerPrefs.SetInt(PrefsKeys.currentGoldKey, 0);
        }
        if (goldUpdate != null) goldUpdate(goldAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

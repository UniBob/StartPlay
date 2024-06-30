using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerPFCKeeper : MonoBehaviour
{
    public delegate void CheckPFCDelegate();
    public static CheckPFCDelegate Check;

    [SerializeField] Vector3 expectedPFC;
    [SerializeField] float expectedKcal;
    Vector3 playerCurrentPFC;
    float currentKilocalories;

    private void Start()
    {
        Check += FinalPFCCheck;
    }
    public void AddPFC(Vector3 PFC, float kcal)
    {
        playerCurrentPFC += PFC;
        currentKilocalories += kcal;
    }

    private void FinalPFCCheck()
    {
        float percentageOfProteinNormClosure = expectedPFC.x / playerCurrentPFC.x;
        if (percentageOfProteinNormClosure < 0.5f)
        {
            AddToParametr(PrefsKeys.bulletDamageKey, -1);
        }
        else
        {
            if (percentageOfProteinNormClosure > 1f)
            {
                AddToParametr(PrefsKeys.bulletDamageKey, 1);
            }
        }

        float percentageOfFatNormClosure = expectedPFC.y / playerCurrentPFC.y;
        if (percentageOfFatNormClosure < 0.5f)
        {
            AddToParametr(PrefsKeys.maxHealthKey, -10);
        }
        else
        {
            if (percentageOfFatNormClosure > 1f)
            {
                AddToParametr(PrefsKeys.maxHealthKey, 10);
            }
        }

        float percentageOfCarbohydratesNormClosure = expectedPFC.z / playerCurrentPFC.z;
        if (percentageOfCarbohydratesNormClosure < 0.5f)
        {
            AddToParametr(PrefsKeys.fireRateKey, -0.5f);
        }
        else
        {
            if (percentageOfFatNormClosure > 1f)
            {
                AddToParametr(PrefsKeys.fireRateKey, 0.5f);
            }
        }

        float percentageOfKCalNormClosure = expectedKcal / currentKilocalories;
        if (percentageOfKCalNormClosure < 0.5f)
        {
            AddToParametr(PrefsKeys.playerMovementSpeed, -0.5f);
        }
        else
        {
            if (percentageOfKCalNormClosure > 1f)
            {
                AddToParametr(PrefsKeys.playerMovementSpeed, 0.5f);
            }
        }
    }
    private void AddToParametr(string key, float increase)
    {
        if (PlayerPrefs.HasKey(key))
        {
            float tmpParametr = PlayerPrefs.GetFloat(key);
            PlayerPrefs.SetFloat(key, tmpParametr + increase);
        }
    }

    private void AddToParametr(string key, int increase)
    {
        if (PlayerPrefs.HasKey(key))
        {
            int tmpParametr = PlayerPrefs.GetInt(key);
            PlayerPrefs.SetInt(key, tmpParametr + increase);
        }
    }
}

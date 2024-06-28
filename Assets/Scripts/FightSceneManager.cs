using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FightSceneManager : MonoBehaviour
{
    [Header("UIPrefabs")]
    [SerializeField] GameObject goldUIElementPrefab;
    [SerializeField] Vector2 goldUIElementCoordinates;
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Vector2 healthBarCoordinates;

    [Header("EnemyesPrefabs")]
    [SerializeField] GameObject[] Enemyes;

    [Header("Stages")]
    [SerializeField] GameObject[] stages;

    [Header("Else")]
    [SerializeField] private int gardenSceneTag = 1;
    [SerializeField] int multiplierPerLevel = 32;

    [SerializeField] private int stage;
    [SerializeField] private int enemyesOnStage;

    private void Start()
    {
        if (PlayerPrefs.HasKey(PrefsKeys.stage))
        {
            stage = PlayerPrefs.GetInt(PrefsKeys.stage);
        }
        else
        {
            stage = 1;
            PlayerPrefs.SetInt(PrefsKeys.stage, stage);
        }

        if (PlayerPrefs.HasKey(PrefsKeys.enemyesOnStage))
        {
            enemyesOnStage = PlayerPrefs.GetInt(PrefsKeys.enemyesOnStage);
        }
        else
        {
            enemyesOnStage = 1;
            PlayerPrefs.SetInt(PrefsKeys.enemyesOnStage, enemyesOnStage);
        }
        AddEnemyesOnStage();
    }

    private void AddEnemyesOnStage()
    {
        int tmpmultiplierPerLevel = multiplierPerLevel * multiplierPerLevel * multiplierPerLevel * multiplierPerLevel;
        int enemyCount = enemyesOnStage;
        while (enemyCount> tmpmultiplierPerLevel)
        {
            Instantiate(Enemyes[3], GetRandomCoordinatesForEnemy(), Quaternion.identity);
            enemyCount -= tmpmultiplierPerLevel;
        }
        tmpmultiplierPerLevel = (int)(tmpmultiplierPerLevel/multiplierPerLevel);
        while (enemyCount > 1024)
        {
            Instantiate(Enemyes[2], GetRandomCoordinatesForEnemy(), Quaternion.identity);
            enemyCount -= 1024;
        }
        tmpmultiplierPerLevel = (int)(tmpmultiplierPerLevel / multiplierPerLevel);
        while (enemyCount > 32)
        {
            Instantiate(Enemyes[1], GetRandomCoordinatesForEnemy(), Quaternion.identity);
            enemyCount -= 32;
        }
        tmpmultiplierPerLevel = (int)(tmpmultiplierPerLevel / multiplierPerLevel);
        while (enemyCount > 1)
        {
            Instantiate(Enemyes[0], GetRandomCoordinatesForEnemy(), Quaternion.identity);
            enemyCount -= 1;
        }
    }

    private Vector2 GetRandomCoordinatesForEnemy()
    {
        return new Vector2(0,0);
    }
    // 
    public void EnemyDeath(int tag)
    {
        int tmp = 1;
        for (int i = tag; i > 0; i--) 
        {
            tmp *= multiplierPerLevel;
        }
        enemyesOnStage -= tmp;
        if (enemyesOnStage <= 0)
        {
            Win();
        }
    }

    private void Win()
    {

        stage++;
        enemyesOnStage = PlayerPrefs.GetInt(PrefsKeys.enemyesOnStage);
        enemyesOnStage += stage * 4;
        PlayerPrefs.SetInt(PrefsKeys.enemyesOnStage, enemyesOnStage);
        PlayerPrefs.SetInt(PrefsKeys.stage, stage);
        SceneManager.LoadScene(gardenSceneTag);
    }
}

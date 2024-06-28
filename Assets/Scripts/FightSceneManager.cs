using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FightSceneManager : MonoBehaviour
{
    [Header("EnemyiesPrefabs")]
    [SerializeField] GameObject[] enemies;

    [Header("Stages")]
    [SerializeField] StageScriptableObject[] stages;

    [Header("Spawn Areas")]
    [SerializeField] private AreaListner[] areas;

    [Header("Else")]
    [SerializeField] private int gardenSceneTag = 1;
    [SerializeField] int multiplierPerLevel = 32;

    [SerializeField] private int stage;
    [SerializeField] private int[] enemiesOnStage;

    int enemiesOnScene = 0;
    int enemiesIterator = 0;
    int spawnTimer = 2;
    float previousSpawnTime;

    private void Start()
    {
        StartingSyncronize();
        AddEnemiesOnStage();
    }

    private void Update()
    {
     if (Time.deltaTime > previousSpawnTime+spawnTimer)
        {
            previousSpawnTime = Time.deltaTime;
            AddEnemiesOnStage();
        }
    }
    private void StartingSyncronize()
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
        enemiesOnStage = stages[stage].enemiesCount;
    }

    private void AddEnemiesOnStage()
    {
        foreach (var item in areas)
        {
            if (item.GetIsEmpty())
            {
                // Преобразуем точку в координаты вьюпорта
                Vector3 viewportPoint = Camera.main.WorldToViewportPoint(item.transform.position);

                // Проверяем, находится ли точка в пределах видимости камеры
                if (!(viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1))
                {
                    if (enemiesOnStage[enemiesIterator] > 0)
                    {
                        enemiesOnStage[enemiesIterator]--;
                        enemiesOnScene++;
                        GameObject instance = Instantiate(enemies[enemiesIterator], item.transform.position, Quaternion.identity);
                    }
                }

            }
        }
    }

    public void EnemyDeath(int tag)
    {
        enemiesOnScene--;
        if (enemiesOnScene <1)
        {
            Win();
        }
    }

    private void Win()
    {

        stage++;
        PlayerPrefs.SetInt(PrefsKeys.stage, stage);
        SceneManager.LoadScene(gardenSceneTag);
    }
}

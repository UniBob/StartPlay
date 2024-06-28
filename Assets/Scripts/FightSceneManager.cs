using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private int stage = 0;
    [SerializeField] private int[] enemiesOnStage;

    int enemiesOnScene = 0;
    int enemiesIterator = 0;
    float spawnTimer = 2f;
    float previousSpawnTime = 0f;

    private void Start()
    {
        StartingSyncronize();
        AddEnemiesOnStage();
    }

    private void Update()
    {
     if (Time.time > previousSpawnTime+spawnTimer)
        {
            previousSpawnTime = Time.time;
            AddEnemiesOnStage();
        }
    }
    private void StartingSyncronize()
    {
        //if (PlayerPrefs.HasKey(PrefsKeys.stage))
        //{
        //    stage = PlayerPrefs.GetInt(PrefsKeys.stage);
        //}
        //else
        //{
        //    stage = 1;
        //    PlayerPrefs.SetInt(PrefsKeys.stage, stage);
        //}
        enemiesOnStage = new int[stages[stage].enemiesCount.Length];
        for(int i = 0;i<enemiesOnStage.Length;i++)
        {
            enemiesOnStage[i] = stages[stage].enemiesCount[i];
        }
    }

    private void AddEnemiesOnStage()
    {
        foreach (var item in areas)
        {
            if (item.GetIsEmpty())
            {
                Debug.Log("Проверило пустоту области");
                // Преобразуем точку в координаты вьюпорта
                Vector3 viewportPoint = Camera.main.WorldToViewportPoint(item.transform.position);

                // Проверяем, находится ли точка в пределах видимости камеры
                if (!(viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1))
                {
                    Debug.Log("Проверило что вне камеры");
                    if (enemiesOnStage[enemiesIterator] > 0)
                    {
                        Debug.Log("Проверило что еще можно спавнить");
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

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightSceneManager : MonoBehaviour
{
    [Header("EnemyiesPrefabs")]
    [SerializeField] GameObject[] enemies;

    [Header("Stages")]
    [SerializeField] StageScriptableObject[] stages;

    [Header("Spawn Areas")]
    [SerializeField] private AreaListner[] areas;

    [Header("Other")]            
    [SerializeField] private int stage = 0;
    [SerializeField] private int[] enemiesOnStage;

    [SerializeField] Slider slider;

    [SerializeField] int enemiesOnScene = 0;
    int enemiesIterator = 0;
    float spawnTimer = 2f;
    float previousSpawnTime = 0f;

    private void Start()
    {
        slider.maxValue = 0;
        StartingSyncronize();
        AddEnemiesOnStage();
    }

    private void Update()
    {
        if (Time.time > previousSpawnTime + spawnTimer)
        {
            previousSpawnTime = Time.time;
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
        enemiesOnStage = new int[stages[stage].enemiesCount.Length];
        for(int i = 0;i<enemiesOnStage.Length;i++)
        {
            enemiesOnStage[i] = stages[stage].enemiesCount[i];
            enemiesOnScene += enemiesOnStage[i];
        }
    }

    private void AddEnemiesOnStage()
    {
        foreach (var item in areas)
        {
            if (item.GetIsEmpty())
            {
                Vector3 viewportPoint = Camera.main.WorldToViewportPoint(item.transform.position);

                if (!(viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1))
                {
                    if (enemiesOnStage[enemiesIterator] > 0)
                    {
                        enemiesOnStage[enemiesIterator]--;
                        GameObject instance = Instantiate(enemies[enemiesIterator], item.transform.position, Quaternion.identity);
                        slider.maxValue += 1;
                        slider.value += 1;
                    }
                    else
                    {
                        if (enemiesIterator<enemiesOnStage.Length) enemiesIterator++;
                    }
                }

            }
        }
    }

    public void EnemyDeath(int tag)
    {
        enemiesOnScene--;
        slider.value -= 1;
        if (enemiesOnScene <1)
        {
            Win();
        }
    }

    private void Win()
    {
        stage++;
        PlayerPrefs.SetInt(PrefsKeys.stage, stage);
        SceneManager.LoadScene(PrefsKeys.dialogueSceneTag);
    }
}

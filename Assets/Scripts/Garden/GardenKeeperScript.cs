using UnityEngine;

public class GardenKeeperScript : MonoBehaviour
{
    public enum Plants
    {
        Nothing,
        Carrot,
        Tomato,
        Cucumber,
        Onion
    }
       
    [SerializeField] PlantsSpotScript[] plantsSpots;
    [SerializeField] Sprite[] plantsSprites;
    //[SerializeField] Plants[] whichPlantIsPlantedInTheSpot;
    [SerializeField] int[] plantsPrices;
    [SerializeField] GoldKeeperScript goldKeeper;
    [SerializeField] GameObject ChoosePlantingPlantPanel;

    [SerializeField] public int[] whichPlantIsPlantedInTheSpot2;
    [SerializeField] PlantsKeeper plantsKeeper;

    private int actualPlantSpot;

    private void Start()
    {
        Player.Save += SaveSpotsStatus;
        //LoadArray(PrefsKeys.plantedKey);
        LoadArray2(PrefsKeys.plantedKey2);
        /*
        if (PlayerPrefs.HasKey(PrefsKeys.clearLocationKey))
        {
            int goldMultiplier = PlayerPrefs.GetInt(PrefsKeys.clearLocationKey);
            if (goldMultiplier > 1)
            {                
                PlayerPrefs.SetInt(PrefsKeys.clearLocationKey, 0);
                int goldIncome = 0;
                for (int i = 0; i < whichPlantIsPlantedInTheSpot.Length; i++)
                {                    
                    if (whichPlantIsPlantedInTheSpot[i] != Plants.Nothing)
                    {
                        goldIncome += plantsPrices[(int)whichPlantIsPlantedInTheSpot[i]];
                    }
                    plantsSpots[i].SetSprite(plantsSprites[(int)Plants.Nothing], true);
                }
                goldKeeper.IncreeseGoldAmount((int)(goldIncome*(goldMultiplier/40)));
            }
            else
            {
                for (int i = 0; i < whichPlantIsPlantedInTheSpot.Length; i++)
                {
                    plantsSpots[i].SetSprite(plantsSprites[(int)whichPlantIsPlantedInTheSpot[i]], whichPlantIsPlantedInTheSpot[i] == Plants.Nothing);
                  
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt(PrefsKeys.clearLocationKey, 0);
            for (int i = 0; i < whichPlantIsPlantedInTheSpot.Length; i++)
            {
                plantsSpots[i].SetSprite(plantsSprites[(int)whichPlantIsPlantedInTheSpot[i]], whichPlantIsPlantedInTheSpot[i] == Plants.Nothing);
            
            }
        }
        */
        plantsKeeper = FindObjectOfType<PlantsKeeper>();

        if (PlayerPrefs.HasKey(PrefsKeys.clearLocationKey))
        {
            for (int i = 0; i < whichPlantIsPlantedInTheSpot2.Length; i++)
            {
                if (whichPlantIsPlantedInTheSpot2[i] != -1)
                {
                    plantsSpots[i].SetSprite(plantsKeeper.allPlants[whichPlantIsPlantedInTheSpot2[i]].sprite, whichPlantIsPlantedInTheSpot2[i] == -1);
                }
                else
                {
                    plantsSpots[i].SetSprite(plantsSprites[(int)Plants.Nothing], true);
                }
            }
        }
    }

    public void SaveSpotsStatus()
    {
        //string json = JsonUtility.ToJson(new Serialization<Plants>(whichPlantIsPlantedInTheSpot));
        string json2 = JsonUtility.ToJson(new Serialization<int>(whichPlantIsPlantedInTheSpot2));
        //PlayerPrefs.SetString(PrefsKeys.plantedKey, json);
        PlayerPrefs.SetString(PrefsKeys.plantedKey2, json2);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    private class Serialization<T>
    {
        [SerializeField]
        private T[] items;

        public Serialization(T[] items)
        {
            this.items = items;
        }

        public T[] ToArray()
        {
            return items;
        }
    }


    /*void LoadArray(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            whichPlantIsPlantedInTheSpot = JsonUtility.FromJson<Serialization<Plants>>(json).ToArray();
        }
    }*/
    void LoadArray2(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            whichPlantIsPlantedInTheSpot2 = JsonUtility.FromJson<Serialization<int>>(json).ToArray();
        }
    }

    public void PlantIsPlanted(int plantId)
    {
        whichPlantIsPlantedInTheSpot2[actualPlantSpot] = plantId;

        plantsSpots[actualPlantSpot].SetSprite(plantsKeeper.allPlants[plantId].sprite, false);

        /*
        whichPlantIsPlantedInTheSpot[actualPlantSpot] = (Plants)plant;
        Debug.Log("plantsSpots[actualPlantSpot]: " + plantsSpots[actualPlantSpot]);
        Debug.Log("plantsSprites[plant]: " + plantsSprites[plant]);
        plantsSpots[actualPlantSpot].SetSprite(plantsSprites[plant], false);
        ChoosePlantingPlantPanel.SetActive(false);
        */
    }

    public void SetActualPlantsSpots(int actualSpot)
    {
        actualPlantSpot = actualSpot;
        //ChoosePlantingPlantPanel.SetActive(true);
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryManager.OpenInventory();
        inventoryManager.isOpenForPlanting = true;
        inventoryManager.isOpen = true;
    }
}

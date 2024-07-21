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

    [SerializeField] public int[] whichPlantIsPlantedInTheSpot;
    [SerializeField] PlantsKeeper plantsKeeper;

    private int actualPlantSpot;

    private void Start()
    {
        Player.Save += SaveSpotsStatus;
        LoadArray(PrefsKeys.plantedKey2);
        
        plantsKeeper = FindObjectOfType<PlantsKeeper>();

        if (PlayerPrefs.HasKey(PrefsKeys.clearLocationKey))
        {
            for (int i = 0; i < whichPlantIsPlantedInTheSpot.Length; i++)
            {
                if (whichPlantIsPlantedInTheSpot[i] != -1)
                {
                    plantsSpots[i].SetSprite(plantsKeeper.allPlants[whichPlantIsPlantedInTheSpot[i]].sprite, whichPlantIsPlantedInTheSpot[i] == -1);
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
        string json2 = JsonUtility.ToJson(new Serialization<int>(whichPlantIsPlantedInTheSpot));
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

    void LoadArray(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            whichPlantIsPlantedInTheSpot = JsonUtility.FromJson<Serialization<int>>(json).ToArray();
        }
    }

    public void PlantIsPlanted(int plantId)
    {
        whichPlantIsPlantedInTheSpot[actualPlantSpot] = plantId;

        plantsSpots[actualPlantSpot].SetSprite(plantsKeeper.allPlants[plantId].sprite, false);
    }

    public void SetActualPlantsSpots(int actualSpot)
    {
        actualPlantSpot = actualSpot;
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryManager.OpenInventory();
        inventoryManager.isOpenForPlanting = true;
        inventoryManager.isOpen = true;
    }
}

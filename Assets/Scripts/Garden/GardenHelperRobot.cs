using UnityEngine;

public class GardenHelperRobot : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private GardenKeeperScript gardenKeeper;
    [SerializeField] GameObject buttonIcon;
    InventoryManager inventoryManager;
    [SerializeField] int[] growingFruits;
    bool isGrowingFruitsReceived = false;
    bool isPhraseActive = false;
    [SerializeField] RobotPhrasesScript robotPhrases;

    private void Start()
    {
        gardenKeeper = FindObjectOfType<GardenKeeperScript>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        robotPhrases = FindObjectOfType<RobotPhrasesScript>();

        growingFruits = gardenKeeper.whichPlantIsPlantedInTheSpot2;
        isGrowingFruitsReceived = false;
        isPhraseActive = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            isPlayerNearby = true;
            buttonIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            isPlayerNearby = false;
            isPhraseActive = false;
            buttonIcon.SetActive(false);
            robotPhrases.ActivatePhrase(false);
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isGrowingFruitsReceived)
        {
            foreach (int fruit in growingFruits)
            {
                inventoryManager.AddItem(fruit, 3, ObjectTypes.plant);
            }

            Debug.Log("Урожай собран");
            isGrowingFruitsReceived = true;


        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isPhraseActive)
        {
            robotPhrases.ActivatePhrase(true);
            isPhraseActive = true;
        }
    }
}

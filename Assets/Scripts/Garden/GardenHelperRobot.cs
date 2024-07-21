using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

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

        growingFruits = gardenKeeper.whichPlantIsPlantedInTheSpot;
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
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrowingFruitsReceived)
            {
                foreach (int fruit in growingFruits)
                {
                    if (fruit > 0) inventoryManager.AddItem(fruit, 3, ObjectTypes.plant);
                }

                Debug.Log("Урожай собран");
                inventoryManager.AddItem(5, 1, ObjectTypes.plant);
                isGrowingFruitsReceived = true;
            }

            if (!isPhraseActive)
            {
                buttonIcon.SetActive(false);
                robotPhrases.ActivatePhrase(true);
                isPhraseActive = true;
            }

            if (!PlayerPrefs.HasKey(PrefsKeys.firstPakageArrived))
            {
                PlayerPrefs.SetInt(PrefsKeys.firstPakageArrived, 0);
                inventoryManager.AddItem(0, 2, ObjectTypes.seed);
                inventoryManager.AddItem(1, 2, ObjectTypes.seed);
                inventoryManager.AddItem(2, 1, ObjectTypes.seed);
                inventoryManager.AddItem(3, 1, ObjectTypes.seed);
                inventoryManager.AddItem(4, 4, ObjectTypes.plant);
            }
        }
    }
}

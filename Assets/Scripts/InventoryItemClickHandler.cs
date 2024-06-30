
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemClickHandler : MonoBehaviour, IPointerClickHandler
{
    PlayerPFCKeeper playerPFCKeeper;
    GardenKeeperScript gardenKeeper;
    InventoryManager inventoryManager;

    private void Start()
    {
        playerPFCKeeper = FindObjectOfType<PlayerPFCKeeper>();
        gardenKeeper = FindObjectOfType<GardenKeeperScript>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnClick()
    {
        InventorySlot slotInfo = GetComponent<InventorySlot>();
        int itemId = slotInfo.itemId;
        ObjectTypes type = slotInfo.type;

        if (itemId == 60000)
        {
            return;
        }

        bool isOpenForPlanting = inventoryManager.isOpenForPlanting;

        if (isOpenForPlanting && type == ObjectTypes.seed)
        {
            //вызов логики посадки
            Debug.Log("Несите навоз ...");
            IncriseItemAmount(slotInfo);
            gardenKeeper.PlantIsPlanted(itemId);
            inventoryManager.CloseInventory();
        }

        if (!isOpenForPlanting && type == ObjectTypes.plant)
        {
            //вызов логики поедания предмета
            Debug.Log("Кушою ... Ням-ням");
            IncriseItemAmount(slotInfo);
            Debug.Log("slotInfo: " + slotInfo.itemId);
            playerPFCKeeper.AddPFCbyItemId(slotInfo.itemId);

        }
    }

    void IncriseItemAmount(InventorySlot slotInfo)
    {
        slotInfo.amount --;
        slotInfo.itemAmount.text = slotInfo.amount.ToString();
    }
}

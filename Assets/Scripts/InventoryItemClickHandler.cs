
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
            //����� ������ �������
            Debug.Log("������ ����� ...");
            gardenKeeper.PlantIsPlanted(itemId);
            inventoryManager.CloseInventory();
            IncriseItemAmount(slotInfo);
        }

        if (!isOpenForPlanting && type == ObjectTypes.plant)
        {
            //����� ������ �������� ��������
            Debug.Log("����� ... ���-���");
            Debug.Log("slotInfo: " + slotInfo.itemId);
            playerPFCKeeper.AddPFCbyItemId(slotInfo.itemId);
            IncriseItemAmount(slotInfo);

        }
    }

    void IncriseItemAmount(InventorySlot slotInfo)
    {
        slotInfo.amount --;
        slotInfo.itemAmount.text = slotInfo.amount.ToString();

        if (slotInfo.amount <= 0)
        {
            slotInfo.itemId = 60000;
            slotInfo.amount = 0;
            slotInfo.type = ObjectTypes.plant;
            slotInfo.isEmpty = true;
            slotInfo.iconGO.sprite = null;
            slotInfo.itemAmount.text = "";
        }
    }
}


using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemClickHandler : MonoBehaviour, IPointerClickHandler
{
    PlayerPFCKeeper playerPFCKeeper;

    private void Start()
    {
        playerPFCKeeper = FindObjectOfType<PlayerPFCKeeper>();
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

        bool isOpenForPlanting = FindObjectOfType<InventoryManager>().isOpenForPlanting;

        if (isOpenForPlanting && type == ObjectTypes.seed)
        {
            //����� ������ �������
            Debug.Log("������ ����� ...");
            IncriseItemAmount(slotInfo);
        }

        if (!isOpenForPlanting && type == ObjectTypes.plant)
        {
            //����� ������ �������� ��������
            Debug.Log("����� ... ���-���");
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

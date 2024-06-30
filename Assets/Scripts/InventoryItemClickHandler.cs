
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemClickHandler : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnClick()
    {
        InventorySlot slotInfo = GetComponent<InventorySlot>();
        int itemId = slotInfo.itemId;

        if (itemId == 60000)
        {
            return;
        }

        PlantTemplate plantSO = FindObjectOfType<PlantsKeeper>().allPlants[itemId];
        ObjectTypes objectType = plantSO.objectType;

        bool isOpenForPlanting = FindObjectOfType<InventoryManager>().isOpenForPlanting;

        if (isOpenForPlanting && objectType == ObjectTypes.seed)
        {
            //вызов логики посадки
            Debug.Log("Несите навоз ...");
        }

        if (!isOpenForPlanting && objectType == ObjectTypes.plant)
        {
            //вызов логики поедания предмета
            Debug.Log("Кушою ... Ням-ням");
        }


    }
}

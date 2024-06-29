using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform inventoryGrid;
    [SerializeField] GameObject inventoryUIPanel;
    PlantsKeeper plantsKeeper;
    public List<InventorySlot> slots = new List<InventorySlot>();
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        plantsKeeper = FindObjectOfType<PlantsKeeper>();
        Player.Save += SaveInventorySlots;
        inventoryUIPanel.SetActive(false);

        for (int i = 0; i < inventoryGrid.childCount; i++)
        {
            InventorySlot inventorySlot = inventoryGrid.GetChild(i).GetComponent<InventorySlot>();
            if (inventorySlot != null)
            {
                inventorySlot.itemId = 60000;
                slots.Add(inventorySlot);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            inventoryUIPanel.SetActive(isOpen);
        }
    }

    public void AddItem(int _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.itemId == _item)
            {
                slot.amount += _amount;
                slot.itemAmount.text = slot.amount.ToString();
                return;
            }
        }
        Debug.Log("set1");
        foreach (InventorySlot slot in slots)
        {

            if (slot.isEmpty)
            {
                slot.itemId = _item;
                slot.amount = _amount;
                slot.SetIcon(plantsKeeper.allPlants[_item].fruitSprite);
                slot.itemAmount.text = _amount.ToString();
                slot.isEmpty = false;
                return;
            }
        }
    }

    private void SaveInventorySlots()
    {
        string json = JsonConvert.SerializeObject(slots, Formatting.Indented);
        Debug.Log(json);
        PlayerPrefs.SetString(PrefsKeys.inventorySlots, json);
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
}

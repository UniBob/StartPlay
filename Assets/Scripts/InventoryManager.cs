using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform inventoryGrid;
    [SerializeField] GameObject inventoryUIPanel;
    [SerializeField] public bool isOpenForPlanting = false;
    PlantsKeeper plantsKeeper;
    public List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] public Vector2Int[] slotsForSave;
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        Player.Save += SaveInventorySlots;

        LoadArray(PrefsKeys.inventorySlots);

        plantsKeeper = FindObjectOfType<PlantsKeeper>();
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

        AddLoadedItemToInventory();
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
        Debug.Log("slots: " + slots.Count);
        foreach (InventorySlot slot in slots)
        {
            Debug.Log("set2");

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
        int index = 0;
        foreach (InventorySlot slot in slots)
        {
            if (slot.itemId != 60000)
            {
                Debug.Log(slot.itemId + "  " + slot.amount);
                Debug.Log("index:  " + index);
                Debug.Log("slotsForSave[]:  " + slotsForSave.Length);
                slotsForSave[index] = new Vector2Int (slot.itemId, slot.amount);
                index++;
            }
        }

        string json = JsonUtility.ToJson(new Serialization<Vector2Int>(slotsForSave));
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

    void LoadArray(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            slotsForSave = JsonUtility.FromJson<Serialization<Vector2Int>>(json).ToArray();
        }
    }

    void AddLoadedItemToInventory()
    {
        foreach (Vector2Int slotData in slotsForSave)
        {
            AddItem(slotData.x, slotData.y);
        }
    }
}

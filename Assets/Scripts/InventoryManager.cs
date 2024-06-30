using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform inventoryGrid;
    [SerializeField] GameObject inventoryUIPanel;
    PlantsKeeper plantsKeeper;
    SeedsKeeper seedsKeeper;
    public List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] public Vector3Int[] slotsForSave;
    public bool isOpen = false;
    public bool isOpenForPlanting = false;

    // Start is called before the first frame update
    void Start()
    {
        Player.Save += SaveInventorySlots;

        LoadArray(PrefsKeys.inventorySlots);

        plantsKeeper = FindObjectOfType<PlantsKeeper>();
        seedsKeeper = FindObjectOfType<SeedsKeeper>();
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

            if (!isOpen)
            {
                isOpenForPlanting = false;
            }
        }
    }

    public void AddItem(int _item, int _amount, ObjectTypes _type)
    {
        if (_amount <= 0)
        {
            return;
        }

        //Debug.Log("_item: " + _item + ", _amount: " + _amount + ", _type: " + _type);
        foreach (InventorySlot slot in slots)
        {
            //Debug.Log("slot.itemId: " + slot.itemId + ", slot.type: " + slot.type);
            if (slot.itemId == _item && slot.type == _type)
            {
                slot.amount += _amount;
                slot.itemAmount.text = slot.amount.ToString();
                return;
            }
        }

        //Debug.Log("Нету схожих в инвентаре");

        foreach (InventorySlot slot in slots)
        {
            //Debug.Log("slot.itemId: " + slot.itemId + ", slot.type: " + slot.type);
            if (slot.isEmpty)
            {
                //Debug.Log("пустой слот");
                slot.itemId = _item;
                slot.amount = _amount;
                slot.type = _type;
                if (slot.type == ObjectTypes.plant)
                {
                    slot.SetIcon(plantsKeeper.allPlants[_item].fruitSprite);
                }
                if (slot.type == ObjectTypes.seed)
                {
                    slot.SetIcon(seedsKeeper.allSeeds[_item].sprite);
                }
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
            if (!slot.isEmpty)
            {
                Debug.Log(slot.itemId + "  " + slot.amount);
                slotsForSave[index] = new Vector3Int (slot.itemId, slot.amount, (int)slot.type);
                index++;
            }
        }

        string json = JsonUtility.ToJson(new Serialization<Vector3Int>(slotsForSave));

        Debug.Log("json:  " + json);
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
            slotsForSave = JsonUtility.FromJson<Serialization<Vector3Int>>(json).ToArray();
        }
    }

    void AddLoadedItemToInventory()
    {
        foreach (Vector3Int slotData in slotsForSave)
        {
            AddItem(slotData.x, slotData.y, (ObjectTypes)slotData.z);
        }
    }

    public void OpenInventory()
    {
        inventoryUIPanel.SetActive(true);
    }
}

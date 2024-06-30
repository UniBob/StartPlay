using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int itemId;
    public int amount;
    public ObjectTypes type;
    public bool isEmpty = true;
    public Image iconGO;
    public TMP_Text itemAmount;

    private void Awake()
    {
       // iconGO = GetComponentInChildren<Image>();
        itemAmount= GetComponentInChildren<TMP_Text>();
    }

    public void SetIcon(Sprite icon)
    {
        iconGO.sprite = icon;
    }
}

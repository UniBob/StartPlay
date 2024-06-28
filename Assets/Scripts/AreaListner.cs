using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AreaListner : MonoBehaviour
{
    bool isEmpty = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isEmpty = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEmpty = true;
    }
    
    public bool GetIsEmpty()
    {
        return isEmpty;
    }
}

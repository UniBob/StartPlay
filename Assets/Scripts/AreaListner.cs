using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaListner : MonoBehaviour
{
    bool isEmpty = false;

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

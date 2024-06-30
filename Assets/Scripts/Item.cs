using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemId;
    public int amount;
    public ObjectTypes type;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Debug.Log("OnTriggerEnter2D");
            FindObjectOfType<InventoryManager>().AddItem(itemId, amount, type);
            Destroy(gameObject);
        }
    }
}

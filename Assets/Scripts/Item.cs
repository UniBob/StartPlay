using UnityEngine;

public class Item : MonoBehaviour
{
    public ObjectTemplate item;
    public int amount;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Debug.Log("OnTriggerEnter2D");
            FindObjectOfType<InventoryManager>().AddItem(item, amount);
            Destroy(gameObject);
        }
    }
}

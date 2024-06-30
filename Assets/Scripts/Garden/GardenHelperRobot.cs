using UnityEngine;

public class GardenHelperRobot : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private GardenKeeperScript keeper;
    [SerializeField] GameObject buttonIcon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            isPlayerNearby = true;
            buttonIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            isPlayerNearby = false;
            buttonIcon.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Урожай собран");
        }
    }
}

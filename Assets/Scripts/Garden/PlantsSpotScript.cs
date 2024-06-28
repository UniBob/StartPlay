using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantsSpotScript : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private GardenKeeperScript keeper;
    private SpriteRenderer spriteRenderer;
    private bool isEmpty = true;
    [SerializeField] private int spotTag;
    [SerializeField] GameObject buttonIcon;

    private void Awake()
    {
        Debug.Log("Awake");
        spriteRenderer = GetComponent<SpriteRenderer>();
        keeper = FindObjectOfType<GardenKeeperScript>();
        buttonIcon.SetActive(false);
    }

    private void Start()
    {
        Debug.Log("Start");
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {            
            PerformAction();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEmpty)
        {
            if (other.GetComponent<Player>() != null)
            {

                isPlayerNearby = true;
                buttonIcon.SetActive(true);
            }
        }
    }

    // Вызывается, когда игрок покидает триггер
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            isPlayerNearby = false;
            buttonIcon.SetActive(false);
        }
    }

    public void SetSprite(Sprite sprite, bool set)
    {
        Debug.Log("spriteRenderer: " + spriteRenderer);
        Debug.Log("spriteRenderer.sprite: " + spriteRenderer.sprite);
        spriteRenderer.sprite = sprite;
        isEmpty = set;
        if(!isEmpty && isPlayerNearby)
        {
            isPlayerNearby = false;
            buttonIcon.SetActive(false);
        }
    }

    // Метод для выполнения действия
    void PerformAction()
    {
        keeper.SetActualPlantsSpots(spotTag);
    }
}

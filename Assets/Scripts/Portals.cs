using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portals : MonoBehaviour
{
    [SerializeField] private int fightSceneTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.Save();
        PlayerPFCKeeper.Check();
        Debug.Log("Something entered the trigger");
        SceneManager.LoadScene(fightSceneTag);
    }
}

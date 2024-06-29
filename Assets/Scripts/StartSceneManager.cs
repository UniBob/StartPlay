using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button startGame;
    [SerializeField] Button resumeGame;
    [SerializeField] Button exit;

    [Header("Other")]
    [SerializeField] GameObject aproveWindow;

    void Start()
    {
        aproveWindow.SetActive(false);
        if (PlayerPrefs.HasKey(PrefsKeys.stage))
        {
            resumeGame.interactable = true;
        }
        else
        {
            resumeGame.interactable = false;
        }
    }

    public void NewGameButton()
    {
        if (resumeGame.interactable)
        {
            aproveWindow.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(PrefsKeys.gardenSceneTag);
        }
    }

    public void ResumeButton()
    {
        SceneManager.LoadScene(PrefsKeys.gardenSceneTag);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void YesButtonAproveWindow()
    {
        PrefsKeys.ResetData();
        SceneManager.LoadScene(PrefsKeys.gardenSceneTag);
    }
    public void NoButtonAproveWindow()
    {
        aproveWindow.SetActive(false);
    }
}

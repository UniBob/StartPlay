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
    [SerializeField] AudioSource audio;

    void Start()
    {
        audio = FindObjectOfType<AudioSource>();
        if (PlayerPrefs.HasKey(PrefsKeys.volumeLvl))
        {
            audio.volume = PlayerPrefs.GetFloat(PrefsKeys.volumeLvl);
        }
        else
        {
            audio.volume = 1.0f;
            PlayerPrefs.SetFloat(PrefsKeys.volumeLvl,audio.volume);
        }
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
            PrefsKeys.ResetData();

            SceneManager.LoadScene(PrefsKeys.dialogueSceneTag);
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
        SceneManager.LoadScene(PrefsKeys.dialogueSceneTag);
    }
    public void NoButtonAproveWindow()
    {
        aproveWindow.SetActive(false);
    }
}

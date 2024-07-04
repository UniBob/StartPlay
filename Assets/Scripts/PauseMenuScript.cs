using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] AudioSource audio;
    [SerializeField] Slider slider;
    [SerializeField] GameObject returnButton;

    private void Start()
    {        
        audio = FindObjectOfType<AudioSource>();

        if (PlayerPrefs.HasKey(PrefsKeys.volumeLvl))
        {
            audio.volume = PlayerPrefs.GetFloat(PrefsKeys.volumeLvl);
            slider.value = audio.volume;
        }
        else
        {
            slider.value = slider.maxValue;
        }
        if (SceneManager.GetActiveScene().buildIndex == PrefsKeys.gardenSceneTag)
        {
            returnButton.SetActive(false);
        }
        else
        {
            returnButton.SetActive(true);
        }

        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    public void PauseButtonClick()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
        
    public void ResumeButtonClick()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnButtonClick()
    {
        SceneManager.LoadScene(PrefsKeys.gardenSceneTag);
    }

    public void SettingsButtonClick()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }    

    public void SaveButtonClick()
    {
        audio.volume = slider.value;
        PlayerPrefs.SetFloat(PrefsKeys.volumeLvl, slider.value);
    }

    public void BackButtonClick()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}

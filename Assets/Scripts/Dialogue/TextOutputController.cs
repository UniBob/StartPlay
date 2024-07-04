using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextOutputController : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;
    [SerializeField] float textOutputDelay;
    [SerializeField] TextScript[] textData;
    [SerializeField] bool isDeathScreen;
    [SerializeField] AudioSource audio;
    int textDataIterator;
    int currentWritingLetter;
    string currentWritingText;
    float lastAddingLetterTime;
    bool isFireButtonPressed = false;

    void Start()
    {
        audio = FindObjectOfType<AudioSource>();
        if (PlayerPrefs.HasKey(PrefsKeys.volumeLvl))
        {
            audio.volume = PlayerPrefs.GetFloat(PrefsKeys.volumeLvl);
        }
        if (isDeathScreen)
        {
            textDataIterator = 0;
        }
        else
        {
            Player.Save += SaveTextProgress;
            if (PlayerPrefs.HasKey(PrefsKeys.textDataPage))
            {
                textDataIterator = PlayerPrefs.GetInt(PrefsKeys.textDataPage);
            }
            else
            {
                textDataIterator = 0;
                PlayerPrefs.SetInt(PrefsKeys.textDataPage, textDataIterator);
            }            
        }
        textBox.text = "";
        currentWritingText = textData[textDataIterator].textboxText;
        currentWritingLetter = 0;
    }
        
    private void SaveTextProgress()
    {
        PlayerPrefs.SetInt(PrefsKeys.textDataPage, textDataIterator);
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isFireButtonPressed)
            {
                isFireButtonPressed = true;
                if (currentWritingLetter < currentWritingText.Length)
                {
                    currentWritingLetter = currentWritingText.Length;
                    textBox.text = currentWritingText;
                }
                else
                {
                    if (textData[textDataIterator].isEndOfDialogie)
                    {
                        textDataIterator++;
                        if (!isDeathScreen)
                        {
                            SaveTextProgress();
                        }
                        SceneManager.LoadScene(PrefsKeys.gardenSceneTag);
                        
                    }
                    textBox.text = "";
                    currentWritingText = textData[++textDataIterator].textboxText;
                    currentWritingLetter = 0;
                }
            }
        }
        else
        {
            isFireButtonPressed = false;
        }

        if (currentWritingLetter < currentWritingText.Length)
        {
            lastAddingLetterTime -= Time.deltaTime;
            if (lastAddingLetterTime <= 0f) 
            {
                textBox.text = textBox.text + currentWritingText[currentWritingLetter++];
                lastAddingLetterTime = textOutputDelay;
            }
        }
    }
}

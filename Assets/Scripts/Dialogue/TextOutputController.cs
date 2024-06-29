using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextOutputController : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;
    [SerializeField] float textOutputDelay;
    [SerializeField] TextScript[] textData;

    int textDataIterator;
    int currentWritingLetter;
    string currentWritingText;
    float lastAddingLetterTime;

    void Start()
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
        if (currentWritingLetter < currentWritingText.Length)
        {
            lastAddingLetterTime -= Time.deltaTime;
            if (lastAddingLetterTime <= 0f) 
            {
                textBox.text = textBox.text + currentWritingText[currentWritingLetter++];
                lastAddingLetterTime = textOutputDelay;
            }
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                textBox.text = "";
                currentWritingText = textData[++textDataIterator].textboxText;
                currentWritingLetter = 0;
            }
        }
    }
}

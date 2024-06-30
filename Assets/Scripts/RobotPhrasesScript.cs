using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotPhrasesScript : MonoBehaviour
{
    [SerializeField] StoreHumor allPhrases;
    TMP_Text text;
    SpriteRenderer renderer;
    public void SetActive(bool active)
    {
        if (active)
        {
            renderer.enabled = true;
            text.gameObject.SetActive(true);
            UpdateText();
        }
        else
        {
            renderer.enabled = false;
            text.gameObject.SetActive(false);
        }
    }
    public void UpdateText()
    {
        text.text = allPhrases.GetPhrase();
    }
}

using TMPro;
using UnityEngine;

public class RobotPhrasesScript : MonoBehaviour
{
    [SerializeField] StoreHumor allPhrases;
    TMP_Text text;
    SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TMP_Text>();
        render.enabled = false;
        text.gameObject.SetActive(false);
    }
    public void ActivatePhrase(bool active)
    {
        if (active)
        {
            render.enabled = true;
            text.gameObject.SetActive(true);
            UpdateText();
        }
        else
        {
            render.enabled = false;
            text.gameObject.SetActive(false);
        }
    }
    public void UpdateText()
    {
        text.text = allPhrases.GetPhrase();
    }
}

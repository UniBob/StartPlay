using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Robot humor")]

public class StoreHumor : ScriptableObject
{
    [SerializeField] string[] phrases;
    public string GetPhrase()
    {
        return phrases[Random.Range(0,phrases.Length)];
    }
}

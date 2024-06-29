using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextData")]

public class TextScript : ScriptableObject
{
    [TextArea(3, 10)]
    [SerializeField] public string textboxText;
}

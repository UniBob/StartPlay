using UnityEngine;

public enum ObjectTypes
{
    plant,
    seed,
    other
}

public class ObjectTemplate : ScriptableObject
{
    public ObjectTypes objectType;
    public string objectName = "Template";
    public string description;
    public int cost;
    public Sprite sprite;
}

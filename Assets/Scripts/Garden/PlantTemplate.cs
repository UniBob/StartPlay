using UnityEngine;

[CreateAssetMenu(fileName = "PlantTemplate", menuName = "TemplateObjects/PlantTemplate")]
public class PlantTemplate : ObjectTemplate
{
    public int fruitCost;
    public Sprite fruitSprite;
    public Vector3 pfc;
    public int calorie;

    private void Start()
    {
        objectType = ObjectTypes.plant;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "SeedTemplate", menuName = "TemplateObjects/SeedTemplate")]
public class SeedTemplate : ObjectTemplate
{
    private void Stert()
    {
        objectType = ObjectTypes.seed;
    }
}

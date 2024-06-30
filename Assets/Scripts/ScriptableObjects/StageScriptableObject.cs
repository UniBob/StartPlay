using UnityEngine;

[CreateAssetMenu(menuName = "Stage data")]

public class StageScriptableObject : ScriptableObject
{
    [SerializeField] public int[] enemiesCount;
}

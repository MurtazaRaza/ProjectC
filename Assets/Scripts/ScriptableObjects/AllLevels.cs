using UnityEngine;

[CreateAssetMenu(fileName = "AllLevels", menuName = "Setup/AllLevel", order = 3)]
public class AllLevels : ScriptableObject
{
    public LevelData[] allLevelsData;
}

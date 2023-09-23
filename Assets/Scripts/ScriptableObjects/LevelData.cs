using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Setup/Level", order = 3)]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public int numberOfCardsToSelect;
    public int totalNumberOfCards;
    public int perSuccessPoint = 1;

    // Using this as a copy constructor of sorts
    public void SetLevelData(LevelData levelData)
    {
        this.levelNumber = levelData.levelNumber;
        this.numberOfCardsToSelect = levelData.numberOfCardsToSelect;
        this.totalNumberOfCards = levelData.totalNumberOfCards;
        this.perSuccessPoint = levelData.perSuccessPoint;
    }
}

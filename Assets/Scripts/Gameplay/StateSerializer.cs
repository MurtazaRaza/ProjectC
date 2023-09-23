using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public static class StateSerializer
{
    public const string CARD_GRID = "CARD_GRID";
    public const string HAS_SAVED_STATE = "HAS_CARD_GRID";
    public const string NUMBER_OF_LEVELS_COMPLETED = "LEVELS_COMPLETED";

    public static int GetNumberOfLevelsCompleted()
    {
        return PlayerPrefs.GetInt(NUMBER_OF_LEVELS_COMPLETED, 1);
    }

    public static void SetLevelCompleted(int val)
    {
        PlayerPrefs.SetInt(NUMBER_OF_LEVELS_COMPLETED, val);
    }
    
    public static bool HasSavedState()
    {
        var val = PlayerPrefs.GetInt(HAS_SAVED_STATE, 0);

        return val == 1;
    }
    
    public static void ClearHasSavedState()
    {
        PlayerPrefs.SetInt(HAS_SAVED_STATE, 0);
    }

    public static void SaveState(CardGameStateSerialized cardGameStateSerialized)
    {
        string data = JsonUtility.ToJson(cardGameStateSerialized);
        SaveStringToFile(data);
        SavedState();
    }

    public static CardGameStateSerialized RetrieveState()
    {
        string data = RetrieveStringFromFile();
        CardGameStateSerialized cardGameStateSerialized = JsonUtility.FromJson<CardGameStateSerialized>(data);

        return cardGameStateSerialized;
    }
    
    private static void SaveStringToFile(string data)
    {
        PlayerPrefs.SetString(CARD_GRID, data);
    }

    private static string RetrieveStringFromFile()
    {
        string data = PlayerPrefs.GetString(CARD_GRID, string.Empty);
        return data;
    }

    private static void SavedState()
    {
        PlayerPrefs.SetInt(HAS_SAVED_STATE, 1);
    }
}

[Serializable]
public struct CardGameStateSerialized
{
    public List<CardSerialized> cardGrid;
    public int levelNumber;
    public int numberOfCardsToSelect;
    public int numberOfCards;
    public int score;
    public int combo;
    public int perSuccessPoint;
}

[Serializable]
public struct CardSerialized
{
    public int cardId;
    public bool isComplete;
    public bool isFlipped;
    
}



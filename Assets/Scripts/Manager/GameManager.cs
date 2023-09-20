using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;
using Utils.BaseClasses;

public class GameManager : UnitySingleton<GameManager>
{
    [SerializeField] 
    private Grid grid;

    [SerializeField] 
    private UiManager uiManager;
    
    private GameState _currentGameState;
    private List<Card> _currentTurnSelectedCards;
    
    // Rules
    private int _numberOfCardsToMatch = 2;

    #region Unity Runtime Events
    
    private void Start()
    {
        // Initialize the grid
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        BroadcastSystem.CardSelected += OnCardSelected;
    }

    private void OnDisable()
    {
        BroadcastSystem.CardSelected -= OnCardSelected;
    }
    
    
    #endregion

    private void OnCardSelected(Card card)
    {
        throw new NotImplementedException();
    }
}

public enum GameState
{
    GameRunning,
    GameEnded
}

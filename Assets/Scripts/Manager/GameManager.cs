using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameEvents;
using UnityEngine;
using Utils.BaseClasses;

public class GameManager : UnitySingleton<GameManager>
{
    [SerializeField] 
    private Grid grid;

    [SerializeField] 
    private UiManager uiManager;

    [SerializeField] 
    private LevelData currentLevelData;
    
    private GameState _currentGameState;
    private List<Card> _currentTurnSelectedCards = new List<Card>();
    
    // Rules
    private int _numberOfCardsToMatch = 2;
    private int _totalNumberOfCards = 4;
    
    private bool _isResume = false;

    #region Unity Runtime Events

    private void Start()
    {
        if (currentLevelData == null)
        {
            Debug.LogError("No level data assigned");
            return;
        }
        
        // Set Rules
        _numberOfCardsToMatch = currentLevelData.numberOfCardsToSelect;
        _totalNumberOfCards = currentLevelData.totalNumberOfCards;
        Scorer.Instance.SetScoreRules(currentLevelData.perSuccessPoint, true);
        
        // Check whether we resumed this level and set isReume

        // Initialize the grid
        if (!_isResume)
        {
            grid.InitializeGrid(_totalNumberOfCards, _numberOfCardsToMatch);
        }
        else
        {
            grid.ReInitializeGrid();
        }
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
        _currentTurnSelectedCards.Add(card);

        if (_currentTurnSelectedCards.Count < _numberOfCardsToMatch) 
            return;

        if (_currentTurnSelectedCards.All(x => x.CardData.id == card.CardData.id))
        {
            MarkAsCorrect();
            StartCoroutine(ResetTurn());
        }
        else
        {
            MarkAsIncorrect();
            StartCoroutine(ResetTurn());
        }
    }

    private void MarkAsCorrect()
    {
        foreach (var card in _currentTurnSelectedCards)
        {
            card.MatchCardSuccess();
        }

        Scorer.Instance.Scored(true);
    }

    private void MarkAsIncorrect()
    {
        foreach (var card in _currentTurnSelectedCards)
        {
            card.MatchCardFailure();
        }
        
        Scorer.Instance.Scored(false);
    }
    
    private IEnumerator ResetTurn()
    {
        BroadcastSystem.CanInput?.Invoke(false);
        _currentTurnSelectedCards.Clear();
        yield return new WaitForSeconds(1f);

        BroadcastSystem.CanInput?.Invoke(true);
    }
}

public enum GameState
{
    GameRunning,
    GameEnded
}

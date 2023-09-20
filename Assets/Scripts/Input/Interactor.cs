using System;
using GameEvents;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public UnityAction MouseEnter;
    public UnityAction MouseExit;
    public UnityAction MouseDown; 
    public UnityAction MouseUp;

    private bool shouldInteract = true;
    
    private void OnEnable()
    {
        BroadcastSystem.GameStateChanged += OnGameStateChanged;
    }
    
    private void OnDisable()
    {
        BroadcastSystem.GameStateChanged -= OnGameStateChanged;
    }
    
    private void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GameEnded:
                shouldInteract = false;
                break;
            case GameState.GameRunning:
                shouldInteract = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }
    
    private void OnMouseEnter()
    {
        if(!shouldInteract) 
            return;
        
        MouseEnter?.Invoke();
    }

    private void OnMouseExit()
    {
        if(!shouldInteract) 
            return;
        
        MouseExit?.Invoke();
    }

    private void OnMouseDown()
    {
        if(!shouldInteract) 
            return;
        
        MouseDown?.Invoke();
    }

    private void OnMouseUp()
    {
        if(!shouldInteract) 
            return;
        
        MouseUp?.Invoke();
    }
}

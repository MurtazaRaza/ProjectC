using System;
using GameEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactor : MonoBehaviour, IPointerClickHandler
{
    public UnityAction MouseEnter;
    public UnityAction MouseExit;
    public UnityAction MouseDown; 
    public UnityAction MouseUp;

    private bool shouldInteract = true;
    
    private void OnEnable()
    {
        BroadcastSystem.GameStateChanged += OnGameStateChanged;
        BroadcastSystem.CanInput += OnCanInputChanged;
    }
    
    private void OnDisable()
    {
        BroadcastSystem.GameStateChanged -= OnGameStateChanged;
        BroadcastSystem.CanInput -= OnCanInputChanged;
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
    
    
    private void OnCanInputChanged(bool val)
    {
        shouldInteract = val;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!shouldInteract) 
            return;
        
        MouseDown?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] 
    private Image cardImage;

    [SerializeField] 
    private GameObject cardFront;

    [SerializeField] 
    private GameObject cardBack;

    public CardData CardData => _cardData;
    
    private bool _isFlipped;
    private CardData _cardData;

    public void PopulateCard(CardData cardData)
    {
        _cardData = cardData;
        UpdateCardUi();
    }

    private void UpdateCardUi()
    {
        throw new System.NotImplementedException();
    }
}

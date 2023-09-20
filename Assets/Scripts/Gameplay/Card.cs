using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;
using UnityEngine.UI;
using Utils;

[RequireComponent(typeof(Interactor))]
public class Card : MonoBehaviour
{

    [SerializeField] 
    private GameObject cardFront;

    [SerializeField]
    private Image cardFrontImage;

    [SerializeField] 
    private GameObject cardBack;

    private Interactor _interactor;

    public CardData CardData => _cardData;
    
    private bool _isFlipped;
    private CardData _cardData;
    

    private void Awake()
    {
        _interactor = GetComponent<Interactor>();
    }

    private void OnEnable()
    {
        _interactor.MouseUp += OnCardTapped;
    }

    private void OnDisable()
    {
        _interactor.MouseUp -= OnCardTapped;
    }


    public void PopulateCard(CardData cardData)
    {
        _cardData = cardData;
        UpdateCardUi();
    }
    
    
    private void OnCardTapped()
    {
        BroadcastSystem.CardSelected?.Invoke(this);
    }

    private async void UpdateCardUi()
    {
        cardFrontImage.sprite = await AssetLoaderUtil.LoadSpriteAsync(_cardData.imageSpriteSoftReference) ??
                                Sprite.Create(Texture2D.whiteTexture,
                                    new Rect(0.0f, 0.0f, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height),
                                    new Vector2(0.5f, 0.5f), 100.0f);
    }
}

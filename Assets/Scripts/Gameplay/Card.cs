using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    public bool IsFlipped => _isFlipped;
    public bool IsComplete => _isComplete;
    
    private bool _isFlipped;
    private bool _isFlipping;
    private bool _isComplete;
    private CardData _cardData;
    

    private void Awake()
    {
        _interactor = GetComponent<Interactor>();
    }

    private void OnEnable()
    {
        _interactor.MouseDown += OnCardTapped;
        BroadcastSystem.UnFlipAllCards += UnFlipCard;
    }

    private void OnDisable()
    {
        _interactor.MouseDown -= OnCardTapped;
        BroadcastSystem.UnFlipAllCards -= UnFlipCard;
    }
    
    public void PopulateCard(CardData cardData)
    {
        _cardData = cardData;
        if(_isFlipped)
            FlipCard(true);
        
        if(_isComplete)
            UpdateCardDisplayAsCompleted();
        //UpdateCardUi();
    }

    public void MatchCardSuccess()
    {
        _isComplete = true;
        // Play Success Animation 
        // Play Matching Sound
        
        // Lerp card to score or smth
        StartCoroutine(MatchCardSuccessRoutine());
    }

    private IEnumerator MatchCardSuccessRoutine()
    {
        yield return new WaitForSeconds(1f);
        
        UpdateCardDisplayAsCompleted();
    }

    public void UpdateCardDisplayAsCompleted()
    {
        cardFront.SetActive(false);
        cardBack.SetActive(false);
    }

    public void MatchCardFailure()
    {
        // Play Not Matching Sound
        // UnFlipCard

        StartCoroutine(MatchCardFailureRoutine());
    }

    private IEnumerator MatchCardFailureRoutine()
    {
        yield return new WaitForSeconds(0.3f);
    }
    
    private void OnCardTapped()
    {
        if(_isFlipped)
            return;
        
        FlipCard(true);
    }

    public void FlipCard(bool val)
    {
        if(_isFlipping)
            return;

        _isFlipping = true;
        _isFlipped = val;

        transform.DOLocalRotate(new Vector3(0, 90, 0), 0.5f, RotateMode.FastBeyond360).SetRelative(true)
            .SetEase(Ease.InOutCubic).onComplete += () =>
        {
            cardBack.gameObject.SetActive(!_isFlipped);
            cardFront.gameObject.SetActive(_isFlipped);
            
            transform.DOLocalRotate(new Vector3(0, 90, 0), 0.3f, RotateMode.Fast).SetRelative(true)
                .SetEase(Ease.OutFlash);
            _isFlipping = false;
            
            BroadcastSystem.CardSelected?.Invoke(this);
        };
    }
    
    private void UnFlipCard()
    {
        if(_isComplete)
            return;
        
        if(_isFlipped)
            FlipCard(false);
    }

    private async void PopulateCardDisplay()
    {
        cardFrontImage.sprite = await AssetLoaderUtil.LoadSpriteAsync(_cardData.imageSpriteSoftReference) ??
                                Sprite.Create(Texture2D.whiteTexture,
                                    new Rect(0.0f, 0.0f, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height),
                                    new Vector2(0.5f, 0.5f), 100.0f);
    }
}

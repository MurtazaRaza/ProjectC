using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityUtilities;
using Utils.AudioUtils;

public class Grid : MonoBehaviour
{
    [SerializeField] 
    private GridLayoutGroup gridLayoutGroup;

    [FormerlySerializedAs("gridData")] 
    [SerializeField]
    private GridData[] gridDatas;

    [SerializeField] 
    private List<CardData> CardDatas;

    [SerializeField] 
    private Card cardPrefab;

    private List<Card> _cardGameObjects = new List<Card>();
    
    public void InitializeGrid(int numberOfCards, int numberOfCardsToSelect)
    {
        SetupGridLayout(numberOfCards);

        List<CardData> listSubsection = new List<CardData>();
        GetSubsectionOfShuffledListToPopulateWith(numberOfCards, numberOfCardsToSelect, ref listSubsection);

        StartCoroutine(PlaceCards(numberOfCardsToSelect, listSubsection));
    }

    private IEnumerator PlaceCards(int numberOfCardsToSelect, List<CardData> cardDatas)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
        for (int i = 0; i < numberOfCardsToSelect; i++)
        {
            foreach (var cardData in cardDatas)
            {
                Card card = Instantiate(cardPrefab.gameObject, gridLayoutGroup.transform).GetComponent<Card>();
                card.PopulateCard(cardData);
                card.gameObject.name = card.CardData.cardName;
                _cardGameObjects.Add(card);

                AudioManager.Play(AudioHolder.Instance.cardPlaceAudio, true);
                yield return waitForSeconds;
            }

            cardDatas.Shuffle();
        }
    }

    private void GetSubsectionOfShuffledListToPopulateWith(int numberOfCards, int numberOfCardsToSelect, ref List<CardData> listSubsection)
    {
        if (numberOfCards % numberOfCardsToSelect != 0)
        {
            Debug.LogError("Number of cards with number of cards to select can never complete");
            throw new Exception("number of cards and number of cards to select mismatch");
        }
        
        List<CardData> cardDatasToManipulate = new List<CardData>(CardDatas);
        cardDatasToManipulate.Shuffle();
        
        listSubsection = cardDatasToManipulate.GetRange(0, (numberOfCards / numberOfCardsToSelect));
    }

    private void SetupGridLayout(int numberOfCards)
    {
        GridData gridData = GetGridDataAccordingToNumberOfCards(numberOfCards);
        gridLayoutGroup.cellSize = gridData.cardSize;
        gridLayoutGroup.constraintCount = gridData.numberOfRows;
        gridLayoutGroup.spacing = gridData.spacing;
    }

    private GridData GetGridDataAccordingToNumberOfCards(int numberOfCards)
    {
        GridData gridDataToUse = gridDatas[0];
        if (numberOfCards <= gridDataToUse.maxCardsThatCanFit)
            return gridDataToUse;
        
        for (var index = 1; index < gridDatas.Length; index++)
        {
            var gridData = gridDatas[index];
            if (numberOfCards < gridData.maxCardsThatCanFit)
                return gridData;
        }

        throw new Exception("Number of cards greater than what is supported");
    }
    
    public void ReInitializeGrid(CardGameStateSerialized cardGameStateSerialized)
    {
        SetupGridLayout(cardGameStateSerialized.numberOfCards);
        List<CardData> cardDataFromSerializedData = new List<CardData>();

        foreach (var cardSerialized in cardGameStateSerialized.cardGrid)
        {
            CardData cardData = CardDatas.Find(card => card.id.Equals(cardSerialized.cardId));
            cardDataFromSerializedData.Add(cardData);
            
            Card card = Instantiate(cardPrefab.gameObject, gridLayoutGroup.transform).GetComponent<Card>();
            card.PopulateCard(cardData);
            card.gameObject.name = card.CardData.cardName;
            
            if(cardSerialized.isComplete)
                card.UpdateCardDisplayAsCompleted();

            if(cardSerialized.isFlipped)
                card.FlipCard(true);
            
            _cardGameObjects.Add(card);
        }
    }

    public List<CardSerialized> SerializeGrid()
    {
        List<CardSerialized> allCardsSerialized = new List<CardSerialized>();
        foreach (var cardGameObject in _cardGameObjects)
        {
            CardSerialized cardSerialized;

            cardSerialized.cardId = cardGameObject.CardData.id;
            cardSerialized.isFlipped = cardGameObject.IsFlipped;
            cardSerialized.isComplete = cardGameObject.IsComplete;

            allCardsSerialized.Add(cardSerialized);
        }

        return allCardsSerialized;
    }

    public bool AreAllCardsComplete()
    {
        foreach (var card in _cardGameObjects)
        {
            if (!card.IsComplete)
                return false;
        }

        return true;
    }
}
